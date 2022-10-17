namespace CarRental.Domain;

public class Product
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double VatPercentage { get; set; }
    public string Condition { get; set; }
    public string Description { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>(); // many-one
}
