using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _service;

        public CarController(ILogger<CarController> logger, ICarService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public IEnumerable<CarController> Get([FromBody] int id)
        {
            var result = _service.Get(id);
            return result;
        }
    }
}
