namespace CarRental.Application.Dto;

public class ClientDataDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int DrivingLicenseObtainingYear { get; set; }
    public ReservatedCarDto CarToRent { get; set; } = new();
}