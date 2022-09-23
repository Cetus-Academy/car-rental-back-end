using CarRentalAPI.DAL;
using CarRentalAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;

namespace CarRentalAPI.Services;

public interface IReservationService
{
    IEnumerable<Car> GetAvailableCars(DateTime startDate, DateTime endDate);
    int[] GetOccupiedDays(int carId, int year, int month);
}
public class ReservationService : IReservationService
{
    private readonly CarDbContext _dbContext;

    public ReservationService(CarDbContext dbContext)//, IMapper mapper
    {
            _dbContext = dbContext;
            //_mapper = mapper;
    }

    public IEnumerable<Car> GetAvailableCars(DateTime startDate, DateTime endDate)
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
            var reservatedCarId = reservation.CarReservations.Select(q => q.Car.Id).ToList();
            carsIds.AddRange(reservatedCarId);
        }
        
        var cars = _dbContext
            .Cars
            .Where(r => !carsIds.Contains(r.Id))
            .ToList();

        return cars;
    }

    public int[] GetOccupiedDays(int carId, int year, int month)
    {
        var car = _dbContext
            .Cars
            //.Include(d => d.);
            .FirstOrDefault(c => c.Id == carId);

        int[] occupiedDays = new int[4];
        occupiedDays[0] = car.Id;

        return occupiedDays;
    }
}
