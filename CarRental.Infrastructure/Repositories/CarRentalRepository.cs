using CarRental.Application.Dto;
using CarRental.Application.Interfaces;
using CarRental.Domain;
using CarRental.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories;

public class CarRentalRepository : ICarRentalRepository
{
    private readonly CarDbContext _dbContext;

    public CarRentalRepository(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //Todo: przerobić tak aby działało to tutuaj
    public Car GetCarAndReservationsByCarId(int carId)
    {
        return new Car();
        // return _dbContext
        //     .Cars
        //     .Include(a => a.CarRental)
        //     .Include(a => a.Reservation)
        //     .FirstOrDefault(a => a.Id == carId);
    }
}