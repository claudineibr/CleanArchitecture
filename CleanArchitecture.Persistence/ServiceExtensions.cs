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
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));

        services.AddDbContext<DataContext>((IServiceProvider sp, DbContextOptionsBuilder builder) =>
        {
            builder.UseSqlite(configuration.GetConnectionString("Sqlite"));
            builder.LogTo(Console.WriteLine, LogLevel.Information);
            builder.EnableDetailedErrors(isDevelopment);
            builder.EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}