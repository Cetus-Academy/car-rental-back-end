namespace CarRental.Domain;

public class CarRental
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public int PricePerDay { get; set; }
    public List<Car> Cars { get; set; }
}