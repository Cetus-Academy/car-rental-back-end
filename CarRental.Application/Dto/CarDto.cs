namespace CarRental.Application.Dto;

public class CarDto
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public int Displacement { get; set; }
    public string FuelType { get; set; }
    public string Description { get; set; }
    public string PriceCategory { get; set; }
    public string Model { get; set; }
    public string Generate { get; set; }
    public int YearOfProduction { get; set; }
    public int Power { get; set; }
    public string Drive { get; set; }
    public int NumberOfDoors { get; set; }
    public int NumberOfPlaces { get; set; }
    public double FuelUsage { get; set; }
    public int CarRentalId { get; set; }
    public int CarBrandId { get; set; }
}