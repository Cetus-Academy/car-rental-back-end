using CarRentalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<CarController>> GetById([FromBody] int id)
    {
        var car = _carService.GetById(id);

        if (car is null)
            return NotFound();

        return Ok(car);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _carService.Delete(id);

        return isDeleted ? NoContent() : NotFound();
    }
}