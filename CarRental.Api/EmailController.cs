using System.Text.RegularExpressions;
using CarRental.Application.Interfaces;
using CarRental.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api;

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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string senderEmail = email.Sender;
        string contactFormMessage = email.Message;

        Regex regExMail = new Regex("[a-zA-Z.0-9]+([+]{1}[a-zA-Z.0-9])?@[a-zA-Z]+[.][a-zA-Z]+");
        Regex regExMessage = new Regex(".+");

        if (!regExMail.IsMatch(senderEmail))
            return BadRequest(new { message = "Podałeś błędny email", emailSend = false });
        if (!regExMessage.IsMatch(contactFormMessage))
            return BadRequest(new { message = "Nie podałeś wiadomości", emailSend = false });

        await _emailSendingRepository.SendFormContactEmail(senderEmail, contactFormMessage);

        return Ok(new { message = "Ok, Wysłano", emailSend = true });
    }
}
