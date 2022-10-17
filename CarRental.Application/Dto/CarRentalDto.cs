namespace CarRental.Application.Dto;

public class CarRentalDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public int PricePerDay { get; set; }
}