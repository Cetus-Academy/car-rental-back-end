namespace CarRental.Domain;

public class Reservations
{
    public int Id { get; set; }
    public string Status { get; set; } //ENUM
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public ICollection<CarReservations> CarReservations { get; set; } = new List<CarReservations>(); //many-one
}

public enum Status
{
    DO_ZAPLATY = 0,
    ZAPLACONO = 1,
    OK = 2,
    ANULOWANO = 3,
    ZAKONCZONO = 4
}
