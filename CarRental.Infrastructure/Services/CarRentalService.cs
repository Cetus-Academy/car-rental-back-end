using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CarRental.Application.Dto;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;

namespace CarRental.Infrastructure.Services;

public class CarRentalService : ICarRentalService
{
    private readonly ICarRentalRepository _carRentalRepository;
    private readonly double _fuelCostPerLiter;
    private readonly ILogger<CarRentalService> _logger;

    public CarRentalService(ICarRentalRepository carRentalRepository, ILogger<CarRentalService> logger,
        double fuelCostPerLiter = 6.8)
    {
        _carRentalRepository = carRentalRepository;
        _logger = logger;
        _fuelCostPerLiter = fuelCostPerLiter;
    }

    public CarRentalResultsDto CalculateRentalPrice(ClientDataDto clientData)
    {
        IsReservationDataValid(clientData);
        var car = _carRentalRepository.GetCarAndReservationsByCarId(clientData.CarToRent.Id);

        if (car is null) throw new NotFoundException("Couldn't find car in db");
        // IsDateAvailable(car, clientData);
        if (car.PriceCategory.ToLower() == "premium")
            HaveDrivingLicenseFor3Years(clientData.DrivingLicenseObtainingYear);


        var carRentPrice =
            DateDiff(clientData.CarToRent.RentFrom, clientData.CarToRent.RentTo) * /*car.CarRental.PricePerDay **/
            GetPriceMultiplier(car.PriceCategory);
        var priceForDrivingLicence = carRentPrice * GetRise(clientData.DrivingLicenseObtainingYear) - carRentPrice;

        //Todo: Add car amount or delete PriceFromCarAmount
        // var priceForCarAmount = (carRentPrice + priceForDrivingLicence) * CheckCarAmount(car.Amount) -
        //                             (carRentPrice + priceForDrivingLicence);
        var fuelCost = GetFuelCost(car.FuelUsage, clientData.CarToRent);

        var results = new CarRentalResultsDto
        {
            CarRentPrice = RoundTo2Decimal(carRentPrice),
            PriceForDrivingLicence = RoundTo2Decimal(priceForDrivingLicence),
            // PriceForCarAmount = RoundTo2Decimal(priceForCarAmount),
            PriceForCarAmount = 0,
            FuelCost = RoundTo2Decimal(fuelCost)
        };

        // SendMailAfterRentingCar(car, clientData, results).Wait();

        return results;
    }

    public CarRentalResultsDto BookCar(ClientDataDto clientData)
    {
        throw new NotImplementedException();
    }

    /*public CarRentalResultsDto BookCar(ClientDataDto clientData)
    {
        var car = CalculateRentalPrice(clientData);
        if (car is null) throw new NotFoundException("Couldn't get car's data");

        var client = new Client()
        {
            FirstName = clientData.FirstName,
            LastName = clientData.LastName,
            City = clientData.City,
            Street = clientData.Street,
            PostalCode = clientData.PostalCode,
            Email = clientData.Email,
            Phone = clientData.Phone,
            DrivingLicenseObtainingYear = clientData.DrivingLicenseObtainingYear
        };

        var reservation = new Reservation()
        {
            RentFrom = clientData.CarToRent.RentFrom,
            RentTo = clientData.CarToRent.RentTo,
            Client = client,
            KmToDrive = clientData.CarToRent.KmToDrive,
            CarId = clientData.CarToRent.Id
        };

        _carRentalRepository.CreateNewReservation(reservation);

        return car;
    }*/

    private static void IsReservationDataValid(ClientDataDto data)
    {
        if (!(IsEmailValid(data.Email) && IsPhoneNumberValid(data.Phone) && AreOnlyLettersInString(data.FirstName) &&
              AreOnlyLettersInString(data.LastName) &&
              IsDrivingLicenceObtainingYearValid(data.DrivingLicenseObtainingYear) &&
              AreDatesValid(data.CarToRent.RentFrom, data.CarToRent.RentTo)))
            throw new BadRequestException("Entered wrong data");
    }

    private static bool IsEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    private static bool IsPhoneNumberValid(string phoneNumber)
    {
        var validatePhoneNumberRegex =
            new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
        return validatePhoneNumberRegex.IsMatch(phoneNumber);
    }

    private static bool IsDrivingLicenceObtainingYearValid(int year)
    {
        return year <= DateTimeOffset.Now.Year;
    }

    private static bool AreDatesValid(DateTimeOffset dateFrom, DateTimeOffset dateTo)
    {
        return dateFrom < dateTo;
    }

    private static bool AreOnlyLettersInString(string text)
    {
        return Regex.IsMatch(text, @"^[a-zA-Z]+$");
    }
    // private static void IsDateAvailable(Car car, ClientDataDto clientData)
    // {
    //     if (car.CarReservations.Any(t => !((clientData.CarToRent.RentFrom > t.RentTo &&
    //                                     clientData.CarToRent.RentTo > t.RentTo) ||
    //                                    (clientData.CarToRent.RentFrom < t.RentFrom &&
    //                                     clientData.CarToRent.RentTo < t.RentFrom))))
    //     {
    //         throw new BadRequestException("This date is already taken");
    //     }
    // }

    // private static async Task SendMailAfterRentingCar(Car car, ClientDataDto clientDataDto,CarRentalResultsDto carRentalResultsDto)
    // {
    //     var apiKey = EmailConfig.ApiKey;
    //     var client = new SendGridClient(apiKey);
    //     var from = new EmailAddress("sendgrid@op.pl", "Test");
    //     var subject = "Sending with SendGrid is Fun";
    //     var to = new EmailAddress(clientDataDto.Email, (clientDataDto.FirstName+" "+clientDataDto.LastName));
    //     var plainTextContent = $"You have just rented {car.Name} and paid {carRentalResultsDto.ResultGross}PLN";
    //     var htmlContent = "<strong>You have just rented "+car.Name+" and paid "+carRentalResultsDto.ResultGross+"PLN</strong>";
    //     var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //     await client.SendEmailAsync(msg);
    // }
    private static double RoundTo2Decimal(double x)
    {
        return Math.Round(x * 100) / 100;
    }

    private double GetFuelCost(double fuelConsumptionPer100Km, ReservatedCarDto data)
    {
        return RoundTo2Decimal(fuelConsumptionPer100Km * (data.KmToDrive / 100) * _fuelCostPerLiter);
    }

    private static double GetPriceMultiplier(string priceCategory)
    {
        var multiplier = priceCategory.ToLower() switch
        {
            "basic" => 1,
            "standard" => 1.3,
            "medium" => 1.6,
            "premium" => 2,
            _ => 0
        };

        return multiplier;
    }

    private static double CheckCarAmount(int amount)
    {
        if (amount < 3)
            return 1.15;
        return 1;
    }

    private static void HaveDrivingLicenseFor3Years(int year)
    {
        if (DateTime.Now.Year - year < 3)
            throw new BadRequestException("Can't rent premium car");
    }

    private static double GetRise(int year)
    {
        if (DateTime.Now.Year - year < 5)
            return 1.2;
        return 1;
    }

    private static double DateDiff(DateTimeOffset date1, DateTimeOffset date2)
    {
        return (date2 - date1).TotalDays;
    }
}
