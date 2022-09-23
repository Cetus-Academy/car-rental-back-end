using System.Text.RegularExpressions;
using CarRentalAPI.Entities;
using CarRentalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers;

[ApiController]
[Route("reservation")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    [HttpGet("avavible-cars")]
    public ActionResult<IEnumerable<Car>> GetAvailableCars([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
    {
        var cars = _reservationService.GetAvailableCars(dateFrom, dateTo);

        return Ok(cars);
    }
    [HttpGet("{carId:int}/{year:int}/{month:int}")]
    public ActionResult<int[]> GetOccupiedDays([FromRoute] int carId, [FromRoute] int year, [FromRoute] int month)
    {
        Regex regExYearAndMonth = new Regex("[0-9]{4}[-_: ][0-9]{1,2}");
        
        if (!regExYearAndMonth.IsMatch(year.ToString()+month.ToString()))
            return BadRequest(new { message = "Podałeś błędną datę lub w", hasSearched = false});
        
        var cars = _reservationService.GetOccupiedDays(carId, year, month);

        return Ok(cars);
    }
}