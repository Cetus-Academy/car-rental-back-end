using AutoMapper;
using CarRental.Application.Dto;
using CarRental.Application.Exceptions;
using CarRental.Domain;
using CarRental.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services
{
    public interface ICarService
    {
        Task<bool> Delete(int id);
        Task<bool> Update(int id, Car car);
        Task<List<Car>> GetAll();
        Task<Car> GetById(int id);
        Task<Car> GetBySlug(string slug);
        Task<int> Create(CarDto carDto);
    }

    public class CarService : ICarService
    {
        private readonly CarDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarService(CarDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            /*var car = await _dbContext.Cars.FindAsync(id);*/ 
            var car = await _dbContext
                .Cars
                .Include(x => x.CarEquipments)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (car is null) return false;

            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(int id, Car car)
        {
            var foundCar = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == id);

            if (foundCar is null) return false;

            foundCar.Slug = car.Slug;
            foundCar.Description = car.Description;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Car>> GetAll()
        {
            var cars = await _dbContext
                .Cars
                .ToListAsync();

            return cars;
        }

        public async Task<Car> GetById(int id)
        {
            var car = await _dbContext
                .Cars
                .FirstOrDefaultAsync(r => r.Id == id);
            if (car is null)
                throw new BadRequestException("Car does not exist in database");
            
            return car;
        }

        public async Task<Car> GetBySlug(string slug)
        {
            var car = await _dbContext
                .Cars
                .FirstOrDefaultAsync(r => r.Slug == slug);
            if (car is null)
                throw new BadRequestException("Car does not exist in database");
            return car;
        }

        public async Task<int> Create(CarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            _dbContext.Cars.Add(car);   
            await _dbContext.SaveChangesAsync();

            return car.Id;
        }
    }
}   
