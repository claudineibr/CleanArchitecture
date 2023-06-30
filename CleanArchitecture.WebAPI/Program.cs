using CleanArchitecture.Application;
using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.WebAPI.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();


builder.AddSerilog(AppName);


builder.Services.ConfigurePersistence(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.ConfigureApplication();
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy(builder.Configuration);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    var ports = GetDefinedPorts(builder.Configuration);
    options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });

});

var app = builder.Build();

var pathBase = app.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseCors();
app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/_proto/", async ctx =>
{
    ctx.Response.ContentType = "text/plain";
    using var fs = new FileStream(Path.Combine(app.Environment.ContentRootPath, "Proto", "navigation.proto"), FileMode.Open, FileAccess.Read);
    using var sr = new StreamReader(fs);
    while (!sr.EndOfStream)
    {
        var line = await sr.ReadLineAsync();
        if (line != "/* >>" || line != "<< */")
        {
            await ctx.Response.WriteAsync(line);
        }
    }
});

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", AppName);
    var serviceScope = app.Services.CreateScope();
    var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
    dataContext?.Database.EnsureCreated();
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", AppName);

    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

(int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
{
    var grpcPort = config.GetValue("GRPC_PORT", 5001);
    var port = config.GetValue("PORT", 80);
    return (port, grpcPort);
}

public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

