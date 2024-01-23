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

//builder.AddServiceDefaults();

builder.Services.AddHealthChecks();
builder.Services.ConfigurePersistence(builder.Configuration, builder.Environment.IsDevelopment());

//builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.ConfigureApplication();
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy(builder.Configuration);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Service - Service HTTP API",
        Version = "v1",
        Description = "The Management Service HTTP API",
        Contact = new OpenApiContact()
        {
            Name = "Claudinei Nascimento",
            Email = "claudinei@nascorp.com.br"
        },
        License = new OpenApiLicense()
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });
});


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

//app.UseServiceDefaults();

app.UseSwagger().UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "Service.API V1");
    setup.OAuthAppName("Management Swagger UI");
});

app.UseRouting();
app.UseErrorHandler();
//app.ConfigureCustomExceptionMiddleware();
app.UseCors();


// Configure GRPC Services


app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

try
{
    app.Logger.LogInformation("Configuring web host ({ApplicationContext})...", AppName);
    var serviceScope = app.Services.CreateScope();
    var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
    dataContext?.Database.MigrateAsync();
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", AppName);
    var logger = app.Services.GetService<ILogger<DataContextSeed>>();
    
    await new DataContextSeed().SeedAsync(context: dataContext, logger: logger);
    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
{
    var grpcPort = config.GetValue("GRPC_PORT", 9101);
    var port = config.GetValue("PORT", 5101);
    return (port, grpcPort);
}

public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

