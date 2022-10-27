using CarRental.Application.Interfaces;
using CarRental.Application.Requests;
using CarRental.Domain;
using CarRental.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly CarDbContext _dbContext;

    public ReservationService(CarDbContext dbContext) //, IMapper mapper
    {
        _dbContext = dbContext;
        //_mapper = mapper;
    }

    public IEnumerable<Car> GetAvailableCarsByDateRange(AvailableCarsRequest request)
    {
        var reservations = _dbContext
            .Reservations
            .Where(d =>
                (request.StartDate <= d.DateFrom && request.StartDate <= d.DateTo)
                ||
                (request.EndDate <= d.DateFrom && request.EndDate <= d.DateTo)
                ||
                (request.StartDate <= d.DateFrom && request.EndDate >= d.DateTo)
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

    public List<int> GetOccupiedDays(int carId, OccupiedDaysRequest request)
    {
        var reservations = _dbContext
            .CarReservations
            .Where(d =>
                (d.Car.Id == carId)
                &&
                (d.Reservations.DateFrom.Year < request.year ||
                 (d.Reservations.DateFrom.Year == request.year && d.Reservations.DateFrom.Month <= request.month))
                &&
                (d.Reservations.DateTo.Year > request.year ||
                 (d.Reservations.DateTo.Year == request.year && d.Reservations.DateTo.Month >= request.month))
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
                if (day.Year == request.year && day.Month == request.month)
                    occupiedDays.Add(day.Day);
            }
        }

        occupiedDays = occupiedDays.Distinct().ToList();

        return occupiedDays;
    }
}