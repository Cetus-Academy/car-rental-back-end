using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI.Entities
{
    public class CarDetails
    {
        //automatic?
        // brand, model...

        public virtual List<CarImages> carImages { get; set; }
    }
}
