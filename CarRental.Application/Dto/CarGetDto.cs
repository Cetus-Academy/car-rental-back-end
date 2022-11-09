namespace CarRentalAPI.Models;

public class CarDto
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public int Displacement { get; set; }
    public string Fuel { get; set; }
    public string Description { get; set; }
}