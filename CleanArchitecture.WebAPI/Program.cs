using CleanArchitecture.Application;
using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.WebAPI.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    config.Sources.Clear();
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
    config.AddEnvironmentVariables();
});
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.AddSerilog(AppName);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    var ports = GetDefinedPorts(builder.Configuration);
//    options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//    });

//    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//    });

//});

builder.Services.ConfigurePersistence(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.ConfigureApplication();
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
dataContext?.Database.EnsureCreated();


app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseCors();
app.MapControllers();
app.Run();

//(int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
//{
//    var grpcPort = config.GetValue("GRPC_PORT", 5001);
//    var port = config.GetValue("PORT", 80);
//    return (port, grpcPort);
//}

public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

