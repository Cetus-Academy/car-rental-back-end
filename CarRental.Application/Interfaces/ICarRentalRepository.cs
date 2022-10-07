using CarRental.Domain;

namespace CarRental.Application.Interfaces;

public interface ICarRentalRepository
{
    Car GetCarAndReservationsByCarId(int id);
}