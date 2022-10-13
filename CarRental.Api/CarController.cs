using CarRental.Application.Dto;
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
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var isDeleted = await _carService.Delete(id);

        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] Car car, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isUpdated = await _carService.Update(id, car);
        
        return isUpdated ? Ok() : NotFound();
    }

    [HttpGet]
    public async Task<OkObjectResult> GetAll()
    {
        var cars = await _carService.GetAll();

        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<OkObjectResult> GetById([FromRoute] int id)
    {
        var car = await _carService.GetById(id);

        return Ok(car);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<Car>> Get([FromRoute] string slug)
    {
        return Ok(await _carService.GetBySlug(slug));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCar([FromBody] CarDto carDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _carService.Create(carDto);

        return Created($"/api/car/{id}", null);
    }
}
