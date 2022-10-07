using EmailService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService;

public static class EmailServiceRegistration
{
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailSendingRepository, EmailSendingRepository>();

        return services;
    }
}
