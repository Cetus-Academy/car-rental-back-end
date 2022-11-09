using System.Text.RegularExpressions;
using CarRental.Application.Exceptions;
using CarRental.Domain;
using CarRental.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services;

public interface IReservationService
{
    Task<IEnumerable<Car>> GetAvailableCarsByDateRange(DateTime startDate, DateTime endDate);
    Task<List<int>> GetOccupiedDays(int carId, int year, int month);
}

public class ReservationService : IReservationService
{
    private readonly CarDbContext _dbContext;

    public ReservationService(CarDbContext dbContext) //, IMapper mapper
    {
        _dbContext = dbContext;
        //_mapper = mapper;
    }

    public async Task<IEnumerable<Car>> GetAvailableCarsByDateRange(DateTime startDate, DateTime endDate)
    {
        var reservations = await ArrayOfReservationsInDateRange(startDate, endDate);

        var carsIds = GetCarIdsFromReservationList(reservations);

        var cars = _dbContext
            .Cars
            .Where(r => !carsIds.Contains(r.Id))
            .ToList();

        return cars;
    }

    private static List<int> GetCarIdsFromReservationList(Reservations[] reservations)
    {
        var carsIds = new List<int>();
        foreach (var reservation in reservations)
        {
            var reservedCarId = reservation.CarReservations.Select(q => q.Car.Id).ToList();
            carsIds.AddRange(reservedCarId);
        }

        return carsIds;
    }

    private async Task<Reservations[]> ArrayOfReservationsInDateRange(DateTime startDate, DateTime endDate)
    {
        var reservations = await _dbContext
            .Reservations
            .Where(d =>
                (startDate <= d.DateFrom && startDate <= d.DateTo)
                ||
                (endDate <= d.DateFrom && endDate <= d.DateTo)
                ||
                (startDate <= d.DateFrom && endDate >= d.DateTo)
            )
            .Include(q => q.CarReservations).ThenInclude(c => c.Car)
            .ToArrayAsync();
        return reservations;
    }

    public async Task<List<int>> GetOccupiedDays(int carId, int year, int month)
    {
        ValidateYearAndMonth(year, month);

        var reservations = await _dbContext
            .CarReservations
            .Where(d =>
                d.Car.Id == carId
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
            .ToArrayAsync();

        var occupiedDays = new List<int>();

        foreach (var reservation in reservations)
            for (var day = reservation.DateFrom.Date; day.Date <= reservation.DateTo.Date; day = day.AddDays(1))
                if (day.Year == year && day.Month == month)
                    occupiedDays.Add(day.Day);

        occupiedDays = occupiedDays.Distinct().ToList();

        return occupiedDays;
    }

    private static void ValidateYearAndMonth(int year, int month)
    {
        var regExYear = new Regex("[0-9]{4}");
        var regExMonth = new Regex("[0-9]{1,2}");

        if (!regExYear.IsMatch(year.ToString()) && !regExMonth.IsMatch(month.ToString()))
            throw new BadRequestException("year/month is not in a valid format");
    }
}
