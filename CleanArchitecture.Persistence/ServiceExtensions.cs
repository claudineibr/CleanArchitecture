namespace CleanArchitecture.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<DataContext>((IServiceProvider sp, DbContextOptionsBuilder builder) =>
        {
            //string connectionString = sp.GetConnectionString(configuration);
            //builder.UseNpgsql(connectionString);

            string connectionName = configuration.GetConnectionString("Default");
            builder.UseNpgsql(configuration.GetConnectionString(connectionName));
            builder.LogTo(Console.WriteLine, LogLevel.Information);
            builder.EnableDetailedErrors(isDevelopment);
            builder.EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped<IUserRepository, UserRepository>();
    }
}