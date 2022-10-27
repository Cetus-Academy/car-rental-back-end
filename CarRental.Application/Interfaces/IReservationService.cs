using CarRental.Domain;

namespace CarRental.Application.Interfaces;

public interface IReservationService
{
    IEnumerable<Car> GetAvailableCarsByDateRange(DateTime startDate, DateTime endDate);
    List<int> GetOccupiedDays(int carId, int year, int month);
}