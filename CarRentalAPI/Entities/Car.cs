using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI.Entities
{
    public class Car
    {
        public int id { get; set; }
        public string slug { get; set; }

        public int carDetailsId { get; set; }
        public virtual CarDetails carDetails { get; set; }
        public virtual List<CarEquipment> carEquipment { get; set; }
    }
}
