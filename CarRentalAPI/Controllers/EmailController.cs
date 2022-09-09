using System.Text.RegularExpressions;
using CarRentalAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers;

[ApiController]
[Route("contact-form")]

public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
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
            return BadRequest(new { message = "Podałeś błędny email", emailSend = false});
        if (!regExMessage.IsMatch(contactFormMessage))
            return BadRequest(new {message = "Nie podałeś wiadomości", emailSend = false});
        
        await _emailService.SendContactFormEmail(senderEmail, contactFormMessage);
        return Ok(new {message = "Ok, Wysłano", emailSend = true});
    }
}