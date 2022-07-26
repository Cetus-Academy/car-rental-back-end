﻿using CarRentalAPI.Entities;
namespace CarRentalAPI.DAL;

public class CarSeeder
{
    private readonly CarDbContext _dbContext;

    public CarSeeder(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Cars.Any())
            {
                var cars = GetCars();
                _dbContext.Cars.AddRange(cars);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Car> GetCars()
    {
        var cars = new List<Car>()
        {
            new Car()
            {
                Slug = "volkswagen-passat-b8",
                Model = "Passat",
                Generate = "B8",
                YearOfProduction = 2018,
                //Mileage = 60 000,
                Displacement = 1998,
                Fuel = "diesel",
                Power = 190,
                Drive = "4x4",
                NumberOfDoors = 5,
                NumberOfPlaces = 5,
                Color = "czarny",
                CountryOfOrigin = "Polska",
                // FirstRegistration = "01/05/2018",
                CarCondition = "używany",
                FuelUsage = 7.5,
                PresentLocation = "Kraków",
                NumberOfAvailableModels = 14,
                Description =
                    "Do wynajęcia Volkswagen Passat B8. Samochód ma około 60 000 przebiegu, jest w stanie dobrym. Kupiony został w Polsce, jest w 100% bezwypadkowy. Wszystkie elementy nadwozia są w oryginalnym lakierze.",
                Images = new List<Image>()
                {
                    new Image(){
                        Src = "https://thumbs.img-sprzedajemy.pl/1000x901c/99/a6/91/volkswagen-passat-b8-kombi-data-produkcji-22-diesel-531704361.jpg",
                        Alt = "Passat B8 zdjęcie"
                    },
                    new Image(){
                        Src = "https://bi.im-g.pl/im/d3/14/10/z16860627IER,Volkswagen-Passat-B8.jpg",
                        Alt = "Passat B8 zdjęcie"
                    },
                    new Image(){
                        Src =
                            "https://thumbs.img-sprzedajemy.pl/1000x901c/5e/b8/09/volkswagen-passat-b8-18-tsi-180-comfortline-swiatla-led-warszawa-527498534.jpg",
                        Alt = "Passat B8 zdjęcie"
                    },
                    new Image(){
                        Src =
                            "https://thumbs.img-sprzedajemy.pl/1000x901c/9a/92/84/volkswagen-passat-b8-18-tsi-180-comfortline-centralny-zamek-mazowieckie-warszawa-527498537.jpg",
                        Alt = "Passat B8 zdjęcie"
                    },
                    new Image(){
                        Src = "https://bi.im-g.pl/im/c9/14/10/z16860617AMP,Volkswagen-Passat-B8.jpg",
                        Alt = "Passat B8 zdjęcie"
                    },
                    new Image(){
                        Src =
                            "https://thumbs.img-sprzedajemy.pl/1000x901c/12/de/f2/volkswagen-passat-comfortline-salon-polska-1wl-manualna-wloclawek-555476152.jpg",
                        Alt = "Passat B8 zdjęcie"
                    }
                },
                CarEquipments = new List<CarEquipment>()
                {
                    new CarEquipment(){
                        Name = "Interfejs Bluetooth"
                    },
                    new CarEquipment(){
                        Name = "Zestaw głośnomówiący"
                    },
                    new CarEquipment(){
                        Name = "Gniazdo USB"
                    },
                    new CarEquipment(){
                        Name = "System nawigacji satelitarnej"
                    },
                    new CarEquipment(){
                        Name = "Klimatyzacja automatyczna (dwustrefowa)"
                    },
                    new CarEquipment(){
                        Name = "Podłokietniki"
                    },
                    new CarEquipment(){
                        Name = "Kierownica ze sterowaniem radia"
                    },
                    new CarEquipment(){
                        Name = "Podgrzewane fotele"
                    },
                    new CarEquipment(){
                        Name = "Kierownica wielofunkcyjna"
                    },
                    new CarEquipment(){
                        Name = "Lusterka boczne ustawiane elektrycznie"
                    },
                    new CarEquipment(){
                        Name = "Asystent świateł drogowych"
                    },
                    new CarEquipment(){
                        Name = "System Start/Stop"
                    },
                    new CarEquipment(){
                        Name = "Oświetlenie wnętrza LED"
                    },
                    new CarEquipment(){
                        Name = "ESP"
                    },
                    new CarEquipment(){
                        Name = "Poduszka powietrzna kierowcy"
                    },
                    new CarEquipment(){
                        Name = "Boczna poduszka powietrzna kierowcy"
                    },
                    new CarEquipment(){
                        Name = "System wspomagania hamowania"
                    }
                }
            },
        };
        return cars;
    }
}