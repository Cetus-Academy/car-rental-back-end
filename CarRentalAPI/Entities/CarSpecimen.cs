using System.ComponentModel;

namespace CarRentalAPI.Entities;

public class CarSpecimen
{
    public int Id { get; set; }
    public string Color { get; set; }
    public string Registration { get; set; }
    public string PresentLocation { get; set; }
    
    public Car Car { get; set; }//one-many
    public ICollection<CarSpecimenReservationIntermediate> CarSpecimenReservationIntermediates { get; set; } = new List<CarSpecimenReservationIntermediate>();//many-one
}