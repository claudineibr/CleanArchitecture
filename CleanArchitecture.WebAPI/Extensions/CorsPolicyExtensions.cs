namespace CleanArchitecture.WebAPI.Extensions;

public static class CorsPolicyExtensions
{
    public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        string[] corsOrigin = configuration.GetSection("CorsOrigin").Get<string[]>();
        services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
    }
}