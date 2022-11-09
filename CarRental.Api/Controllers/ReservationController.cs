using CarRental.Application.Requests;
using CarRental.Domain;
using CarRental.Infrastructure.Services;
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
        var cars = _reservationService.GetOccupiedDays(carId, request);

        return Ok(cars);
    }
}
