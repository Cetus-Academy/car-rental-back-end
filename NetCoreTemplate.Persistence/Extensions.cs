using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreTemplate.Persistence.EF.Context;

namespace NetCoreTemplate.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReadDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"),
                    opt => { opt.CommandTimeout(30); }).LogTo(Console.WriteLine, new[]
                {
                    DbLoggerCategory.Database.Command.Name,
                }, LogLevel.Information)
                .EnableSensitiveDataLogging();

        });
        
        services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"),
                    opt => { opt.CommandTimeout(30); }).LogTo(Console.WriteLine, new[]
                {
                    DbLoggerCategory.Database.Command.Name,
                }, LogLevel.Information)
                .EnableSensitiveDataLogging();

        });
        return services;
    }
}