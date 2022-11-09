namespace CarRental.Domain;

public class CarLocation
{
    public int Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public string Location { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public Car Car { get; set; } //one-many
}