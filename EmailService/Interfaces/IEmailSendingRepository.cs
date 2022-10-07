using System.Net;
using CarRental.Application.Shared;
using CarRental.Shared.Services;
using EmailService.Common;
using EmailService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailService.Interfaces;

public interface IEmailSendingRepository
{
    Task SendFormContactEmail(string email, string message);
    //TODO: add a confiramtion e-mail
}

public class EmailSendingRepository : IEmailSendingRepository
{
    private readonly IRazorViewRenderer _razorViewRenderer;

    public EmailSendingRepository(IRazorViewRenderer razorViewRenderer)
    {
        _razorViewRenderer = razorViewRenderer;
    }

    public async Task SendFormContactEmail(string email, string message)
    {
        var model = new CarRentalNotificationModel()
        {
            Subject = EmailSettings.Message,
            Email = email,
            SenderMessage = message,
            PlainContent = "mail"
        };
        await Send(model);
    }

    async Task Send(EmailModel model)
    {
        var viewName = @"Views/Emails/ContactEmail.cshtml";

        var renderedView = await _razorViewRenderer.RenderViewToStringAsync(viewName, model);

        model.HtmlContent = renderedView;

        var client = new SendGridClient(EmailSettings.ApiKey);
        var from = new EmailAddress(EmailSettings.Email, EmailSettings.Message);
        var receiver = new EmailAddress(model.Email);

        var msg = MailHelper.CreateSingleEmail(from, receiver, model.Subject, model.PlainContent,
            model.HtmlContent);

        var response = await client.SendEmailAsync(msg);

        if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
            throw new Exception("nie udało się");
    }
}
