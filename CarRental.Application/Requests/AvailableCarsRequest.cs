namespace CarRental.Application.Requests;

public class AvailableCarsRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
