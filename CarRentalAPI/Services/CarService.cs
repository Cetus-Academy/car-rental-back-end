using AutoMapper;
using CarRentalAPI.Controllers;
using CarRentalAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalAPI.DAL;

namespace CarRentalAPI.Services
{
    public interface ICarService
    {
        Car GetById(int id);
        bool Delete(int id);
        //Car GetById(int id);
        //CarDto GetById(int id);
        //IEnumerable<CarDto> GetAll();
        //int Create(CreateCarDto dto);
        //bool Delete(int id);
        //bool Update(int id, UpdateCarDto dto);
    }
    public class CarService : ICarService
    {
        private readonly CarDbContext _dbContext = null!;

        public CarService(CarDbContext dbContext)//, IMapper mapper
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }

        private static readonly string[] brands = new[]
        {
            "Audi", "Ford", "Mazda", "Mercedes-Benz", "Opel", "Porsche", "Škoda", "Volkswagen"
        };
        private static readonly string[] fuelTypes = new[]
        {
            "benzyna", "diesel", "LPG"
        };
        private static readonly string[] priceCategory = new[]
        {
            "Basic", "Standard", "Medium", "Premium"
        };

        //public Car GetAll()
        //{
        //    var car = _dbContext
        //        .Cars
        //        .Include(r => r.Slug)
        //        .FirstOrDefault(r => r.Id == id);
//
        //    if (car is null) return null;
//
        //    var result = car;
        //    return result;
        //    //throw new NotImplementedException();
        //    //return null;
        //}
        public Car GetById(int id)
        {
            var car = _dbContext
                .Cars
                .Include(r => r.Slug)
                .FirstOrDefault(r => r.Id == id);

            if (car is null) return null;

            var result = car;
            return result;
            //throw new NotImplementedException();
            //return null;
        }
        public bool Delete(int id)
        {
            //var car = _dbContext
            //    .Cars
            //    .FirstOrDefault(r => r.Id == id);
            var car = _dbContext.Cars.Find(id);

            if (car is null) return false;

            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
