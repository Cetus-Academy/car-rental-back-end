using System.Text.RegularExpressions;
using CarRental.Application.Interfaces;
using CarRental.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api;

[ApiController]
[Route("reservation-module")]
public class CarReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public CarReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("avaible-cars")]
    public ActionResult<IEnumerable<Car>> GetAvailableCars([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
    {
        var cars = _reservationService.GetAvailableCarsByDateRange(dateFrom, dateTo);

        return Ok(cars);
    }

    [HttpGet("car/{carId:int}/occupied-days")]
    public ActionResult<int[]> GetOccupiedDays([FromRoute] int carId, [FromQuery] int year, [FromQuery] int month)
    {
        Regex regExYear = new Regex("[0-9]{4}");
        Regex regExmonth = new Regex("[0-9]{1,2}");

        if (!regExYear.IsMatch(year.ToString()) && !regExYear.IsMatch(month.ToString()))
            return BadRequest(new { message = "Podałeś błędny rok lub datę", hasSearched = false });

        var cars = _reservationService.GetOccupiedDays(carId, year, month);

        return Ok(cars);
    }
}
