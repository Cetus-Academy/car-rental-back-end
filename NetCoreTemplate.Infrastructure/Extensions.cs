using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetCoreTemplate.Application.Common.Identity;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Application.Repositories;
using NetCoreTemplate.Infrastructure.Common.Services;
using NetCoreTemplate.Infrastructure.Common.Services.IdentityService;
using NetCoreTemplate.Infrastructure.Repositories;

namespace NetCoreTemplate.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IDateService, DateService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReportRepository, ReportRepository>();

        return services;
    }
}