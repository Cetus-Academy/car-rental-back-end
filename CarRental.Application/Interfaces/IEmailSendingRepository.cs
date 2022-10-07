namespace CarRental.Application.Interfaces;

public interface IEmailSendingRepository
{
    Task SendFormContactEmail(string email, string message); 
    //TODO: add a confirmation e-mail
}