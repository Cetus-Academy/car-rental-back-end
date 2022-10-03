namespace CarRentalAPI.Entities;

public class CarBrand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>(); // many-one
    public ICollection<Image> Images { get; set; } = new List<Image>(); // many-one
}
