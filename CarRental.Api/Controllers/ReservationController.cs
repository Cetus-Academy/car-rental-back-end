using System.Text.RegularExpressions;
using CarRental.Application.Interfaces;
using CarRental.Application.Requests;
using CarRental.Domain;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

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
    public ActionResult<IEnumerable<Car>> GetAvailableCars([FromQuery] AvailableCarsRequest request)
    {
        var cars = _reservationService.GetAvailableCarsByDateRange(request);

        return Ok(cars);
    }

    [HttpGet("car/{carId:int}/occupied-days")]
    public ActionResult<int[]> GetOccupiedDays([FromRoute] int carId, [FromQuery] OccupiedDaysRequest request)
    {
        Regex regExYear = new Regex("[0-9]{4}");
        Regex regExmonth = new Regex("[0-9]{1,2}");

        if (!regExYear.IsMatch(request.year.ToString()) && !regExYear.IsMatch(request.month.ToString()))
            return BadRequest(new { message = "Podałeś błędny rok lub datę", hasSearched = false });

        var cars = _reservationService.GetOccupiedDays(carId, request);

        return Ok(cars);
    }
}
