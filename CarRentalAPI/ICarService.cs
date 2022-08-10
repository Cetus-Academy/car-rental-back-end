using CarRentalAPI.Controllers;
using CarRentalAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI
{
    public interface ICarService
    {
        IEnumerable<CarController> Get(int id);
    }
}
