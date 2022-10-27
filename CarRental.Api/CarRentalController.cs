using CarRental.Application.Dto;
using CarRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API;

[Route("api/carRental")]
public class CarRentalController : ControllerBase
{
    private readonly ICarRentalService _service;
    public CarRentalController(ICarRentalService service)
    {
        _service = service;
    }

    [HttpPost("")]
    public ActionResult<CarRentalResultsDto> CalculateRentalPrice([FromBody] ClientDataDto clientData)
    {
        var returnData = _service.CalculateRentalPrice(clientData);
        return Ok(returnData);
    }

    [HttpPost("book")]
    public ActionResult<CarRentalResultsDto> BookCar([FromBody] ClientDataDto clientData)
    {
        var returnData = _service.BookCar(clientData);
        return Ok(returnData);
    }
}   