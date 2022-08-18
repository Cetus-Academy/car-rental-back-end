namespace CarRentalAPI.Entities;

public class Car
{
    public int Id { get; set; }
    public string Slug { get; set; }
    //public string Name { get; set; }//TODO: fix when DTO (is ready) slug needs to be automaticly generated from name, slug can be overwritten
        
    public int Displacement { get; set; }
    public string Fuel { get; set; }
    public string Description { get; set; }
    public string PriceCategory { get; set; }
    public string Model { get; set; }
    public string Generate { get; set; }
    public int YearOfProduction { get; set; }
    public int Power{ get; set; }
    public string Drive { get; set; }
    public int NumberOfDoors { get; set; }
    public int NumberOfPlaces { get; set; }
    public string Color { get; set; }
    public string CountryOfOrigin { get; set; }
    public string CarCondition { get; set; }
    public double FuelUsage { get; set; }
    public string PresentLocation { get; set; }
    public int NumberOfAvailableModels { get; set; }
    
    public CarBrand CarBrand { get; set; }// one-many
    public ICollection<Image> Images { get; set; } = new List<Image>();// many-one
    public ICollection<CarEquipment> CarEquipments { get; set; } = new List<CarEquipment>();// many-one
}