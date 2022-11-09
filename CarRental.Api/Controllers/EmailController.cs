using CarRental.Application.Interfaces;
using CarRental.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("contact-form")]
public class EmailController : ControllerBase
{
    private readonly IEmailSendingRepository _emailSendingRepository;

    public EmailController(IEmailSendingRepository mailSendingRepository)
    {
        _emailSendingRepository = mailSendingRepository;
    }

    [HttpPost]
    public async Task<ActionResult> SendContactFormEmail([FromBody] Email email)
    {
        var senderEmail = email.Sender;
        var contactFormMessage = email.Message;

        await _emailSendingRepository.SendFormContactEmail(senderEmail, contactFormMessage);

        return NoContent();
    }
}