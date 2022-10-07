using CarRental.Domain;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api;

[ApiController]
[Route("cars")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _carService.Delete(id);

        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromBody] Car car, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isUpdated = _carService.Update(id, car);

        return isUpdated ? Ok() : NotFound();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Car>> GetAll()
    {
        var cars = _carService.GetAll();

        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Car> GetById([FromRoute] int id)
    {
        var car = _carService.GetById(id);

        return car is null ? NotFound() : Ok(car);
    }

    [HttpGet("{slug}")]
    public ActionResult<Car> Get([FromRoute] string slug)
    {
        var car = _carService.GetBySlug(slug);

        return car is null ? NotFound() : Ok(car);
    }

    [HttpPost]
    public ActionResult CreateCar([FromBody] Car car)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int id = _carService.Create(car);

        return Created($"/api/car/{id}", null);
    }
}
