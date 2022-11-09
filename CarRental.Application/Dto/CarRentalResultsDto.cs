namespace CarRental.Application.Dto;

public class CarRentalResultsDto
{
    public double CarRentPrice { get; set; }
    public double PriceForDrivingLicence { get; set; }
    public double PriceForCarAmount { get; set; }
    public double FuelCost { get; set; }
    public double ResultNet => Math.Round(CarRentPrice + PriceForDrivingLicence + PriceForCarAmount + FuelCost);
    public double ResultGross => Math.Round(ResultNet * 1.23 * 100) / 100;
}