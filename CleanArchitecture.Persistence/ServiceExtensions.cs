using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<DataContext>((IServiceProvider sp, DbContextOptionsBuilder builder) =>
        {
            BuilderDataBase(configuration, builder);
            builder.LogTo(Console.WriteLine, LogLevel.Information);
            builder.EnableDetailedErrors(isDevelopment);
            builder.EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void BuilderDataBase(IConfiguration configuration, DbContextOptionsBuilder builder)
    {
        string connectionString = configuration["ConnectionString"];

        if (string.IsNullOrEmpty(connectionString))
            connectionString = configuration.GetConnectionString("Default");

        if (configuration["ServeDataBase"] == "MYSQL")
        {
            builder.UseSqlite(connectionString);
            return;
        }

        builder.UseNpgsql(connectionString);
    }
}