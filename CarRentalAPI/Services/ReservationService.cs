using CarRentalAPI.DAL;
using CarRentalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Services;

public interface IReservationService
{
    IEnumerable<Car> GetAvailableCarsByDateRange(DateTime startDate, DateTime endDate);
    List<int> GetOccupiedDays(int carId, int year, int month);
}

public class ReservationService : IReservationService
{
    private readonly CarDbContext _dbContext;

    public ReservationService(CarDbContext dbContext) //, IMapper mapper
    {
        _dbContext = dbContext;
        //_mapper = mapper;
    }

    public IEnumerable<Car> GetAvailableCarsByDateRange(DateTime startDate, DateTime endDate)
    {
        var reservations = _dbContext
            .Reservations
            .Where(d =>
                (startDate <= d.DateFrom && startDate <= d.DateTo)
                ||
                (endDate <= d.DateFrom && endDate <= d.DateTo)
                ||
                (startDate <= d.DateFrom && endDate >= d.DateTo)
            )
            .Include(q => q.CarReservations).ThenInclude(c => c.Car)
            .ToArray();

        var carsIds = new List<int>();
        foreach (var reservation in reservations)
        {
            var reservedCarId = reservation.CarReservations.Select(q => q.Car.Id).ToList();
            carsIds.AddRange(reservedCarId);
        }

        var cars = _dbContext
            .Cars
            .Where(r => !carsIds.Contains(r.Id))
            .ToList();

        return cars;
    }

    public List<int> GetOccupiedDays(int carId, int year, int month)
    {
        var reservations = _dbContext
            .CarReservations
            .Where(d =>
                (d.Car.Id == carId)
                &&
                (d.Reservations.DateFrom.Year < year ||
                 (d.Reservations.DateFrom.Year == year && d.Reservations.DateFrom.Month <= month))
                &&
                (d.Reservations.DateTo.Year > year ||
                 (d.Reservations.DateTo.Year == year && d.Reservations.DateTo.Month >= month))
            )
            .Select(r => new
            {
                r.Reservations.DateFrom,
                r.Reservations.DateTo
            })
            .ToArray();

        var occupiedDays = new List<int>();

        foreach (var reservation in reservations)
        {
            for (var day = reservation.DateFrom.Date; day.Date <= reservation.DateTo.Date; day = day.AddDays(1))
            {
                if (day.Year == year && day.Month == month)
                    occupiedDays.Add(day.Day);
            }
        }

        occupiedDays = occupiedDays.Distinct().ToList();

        return occupiedDays;
    }
}
