using CleanArchitecture.Application.Repositories;
namespace CleanArchitecture.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<DataContext>((IServiceProvider sp, DbContextOptionsBuilder builder) =>
        {
            //string connectionName = sp.IsInDocker() ? "Docker" : "Default"; Use Library to refence
            string connectionName = configuration.GetConnectionString("Default");
            builder.UseNpgsql(configuration.GetConnectionString(connectionName));
            builder.LogTo(Console.WriteLine, LogLevel.Information);
            builder.EnableDetailedErrors(isDevelopment);
            builder.EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped<IUserRepository, UserRepository>();
    }
}