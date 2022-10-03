using System.ComponentModel;

namespace CarRentalAPI.Entities;

public class CarReservations
{
    public int Id { get; set; }
    public Car Car { get; set; } //one-many
    public Reservations Reservations { get; set; } //many-one
}
