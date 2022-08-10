using CarRentalAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI
{
    public class CarService : ICarService
    {
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

        public IEnumerable<CarController> Get(int id)
        {
            
        }
    }
}
