using CarRental.Application.Requests;
using CarRental.Domain;

namespace CarRental.Application.Interfaces;

public interface IReservationService
{
    IEnumerable<Car> GetAvailableCarsByDateRange(AvailableCarsRequest request);
    List<int> GetOccupiedDays(int carId, OccupiedDaysRequest request);
}