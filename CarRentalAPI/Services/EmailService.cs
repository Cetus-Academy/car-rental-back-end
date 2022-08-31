namespace CarRentalAPI.Services;
using CarRentalAPI.DAL;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
public interface IEmailService
{
    Task SendContactFormEmail(string senderEmail, string contactFormMessage);
}
public class EmailService : IEmailService
{
    private readonly CarDbContext _dbContext;

    public EmailService(CarDbContext dbContext)//, IMapper mapper
    {
        _dbContext = dbContext;
        //_mapper = mapper;
    }

    public async Task SendContactFormEmail(string senderEmail, string contactFormMessage)
    {
        await SendContactFormEmailReceive(senderEmail, contactFormMessage);
        await SendContactFormEmailConfirmation(senderEmail, contactFormMessage);
    }
    private async Task SendContactFormEmailReceive(string senderEmail, string contactFormMessage)
    {
        var apiKey = "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";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cet.us.autonomus@gmail.com", "Fifonż Kowalski");
        var subject = "Contact";
        var to = new EmailAddress(senderEmail, "Contact CarRental");
        var plainTextContent = contactFormMessage;
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
    private async Task SendContactFormEmailConfirmation(string senderEmail, string contactFormMessage)
    {
        var apiKey = "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";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cet.us.autonomus@gmail.com", "Fifonż Kowalski");
        var subject = "Contact Confirmation";
        var to = new EmailAddress(senderEmail, "Contact CarRental");
        var plainTextContent = contactFormMessage+"\n Dziękujemy za kontakt";
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}