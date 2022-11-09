namespace CarRental.Domain;

public class Car
{
    public int Id { get; set; }
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

    public CarRental CarRental { get; set; }
    public int CarRentalId { get; set; }
    public CarBrand CarBrand { get; set; } // one-many
    public int CarBrandId { get; set; }
    public ICollection<CarReservations> CarReservations { get; set; } = new List<CarReservations>(); //manu-one
    public ICollection<CarLocation> CarLocation { get; set; } = new List<CarLocation>(); // many-one
    public ICollection<Image> Images { get; set; } = new List<Image>(); // many-one
    public ICollection<CarEquipment> CarEquipments { get; set; } = new List<CarEquipment>(); // many-one
}

public enum FuelType
{
    LPG = 0,
    LPG_AND_PETROL = 1,
    PETROL = 2,
    DIESEL = 3
}