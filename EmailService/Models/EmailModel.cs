namespace EmailService.Models;

public class EmailModel
{
    public string PlainContent { get; set; }
    public string HtmlContent { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
}