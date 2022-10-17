using CarRental.Application.Dto;

namespace CarRental.Application.Services;

public interface ICarRentalService
{
    CarRentalResultsDto CalculateRentalPrice(ClientDataDto clientData);
    CarRentalResultsDto BookCar(ClientDataDto clientData);
}