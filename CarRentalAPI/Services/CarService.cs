﻿using AutoMapper;
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
        bool Delete(int id);
        bool Update(int id, Car car);
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        Car GetBySlug(string slug);
        //Car GetById(int id);
        //CarDto GetById(int id);
        //IEnumerable<CarDto> GetAll();
        //int Create(CreateCarDto dto);
        //bool Delete(int id);
        //bool Update(int id, UpdateCarDto dto);
        int Create(Car car);
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
        public bool Update(int id, Car car)
        {
            var restaurant = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;

            restaurant.Slug = car.Slug;
            restaurant.Description = car.Description;
            //TODO: ask what rows can be changed

            _dbContext.SaveChanges();

            return true;
        }
        public IEnumerable<Car> GetAll()
        {
            var cars = _dbContext
                .Cars
                .ToList();

            return cars;
            //if (cars is null) return null;
            //var result = cars;
            //return result;
            //throw new NotImplementedException();
            //return null;
        }
        public Car GetById(int id)
        {
            var car = _dbContext
                .Cars
                //.Include(r => r.Slug)
                .FirstOrDefault(r => r.Id == id);

            return car;//if car is null then return null
        }
        public Car GetBySlug(string slug)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Slug == slug);

            return car;//if car is null then return null
        }
        public int Create(Car car)
        {
            //var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return car.Id;
        }
    }
}