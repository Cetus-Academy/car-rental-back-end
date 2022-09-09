using System.Reflection;
using EmailService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService;

public static class EmailServiceRegistration
{
    public static IServiceCollection AddEmailService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEmailSendingRepository, EmailSendingRepository>();
        
        return services;
        
    }
}