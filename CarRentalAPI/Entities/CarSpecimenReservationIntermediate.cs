using System.Collections.IEnumerable;

namespace CarRentalAPI.Entities;

public class CarSpecimenReservationIntermediate
{
    public int Id { get; set; }
    
    public CarSpecimen CarSpecimen { get; set; }//one-many
    public CarSpecimenReservation CarSpecimenReservation { get; set; }//one-many
}