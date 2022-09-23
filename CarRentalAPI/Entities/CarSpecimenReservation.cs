namespace CarRentalAPI.Entities;

public class CarSpecimenReservation
{
    public int Id { get; set; }
    //public  Status { get; set; }//ENUM
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public ICollection<CarSpecimenReservationIntermediate> CarSpecimenReservationIntermediates { get; set; } = new List<CarSpecimenReservationIntermediate>();//many-one
}