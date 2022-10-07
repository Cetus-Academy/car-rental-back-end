using CarRental.Domain;

namespace CarRental.Infrastructure.DAL;

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
                var carsAndCarRentals = GetCarsAndCarRentals();
                var products = GetProducts();
                _dbContext.Products.AddRange(products);
                _dbContext.CarRentals.AddRange(carsAndCarRentals);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Product> GetProducts()
    {
        var products = new List<Product>()
        {
            new Product()
            {
                Slug = "naklejki-na-samochod-10-szt",
                Name = "Naklejki na samochód 10 szt.",
                Price = 25.99,
                VatPercentage = 0.23,
                Condition = "nowy",
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Src =
                            "https://a.allegroimg.com/original/11ce9c/6d70593b4015b139321a2344b6cf/Zestaw-naklejek-na-samochod-LAMINOWANE-UV",
                        Alt = "Zestaw naklejek na samochód"
                    }
                }
            },
        };
        return products;
    }

    private Domain.CarRental GetCarsAndCarRentals()
    {
        var results = new Domain.CarRental()
        {
            Name = "Car Rental Fast and Furious",
            City = "Reszów",
            Street = "Makłowicza 13",
            PostalCode = "00-000",
            PricePerDay = 100,
            Cars = new List<Car>()
            {
                new Car()
                {
                    Slug = "volkswagen-passat-b8",
                    Name = "volkswagen passat b8",
                    Model = "Passat",
                    Generate = "B8",
                    YearOfProduction = 2018,
                    //Mileage = 60 000,
                    Displacement = 1998,
                    //FuelType = 2,
                    FuelType = "DIESEL",
                    Power = 190,
                    Drive = "4x4",
                    NumberOfDoors = 5,
                    NumberOfPlaces = 5,
                    //CountryOfOrigin = "Polska",
                    FuelUsage = 7.5,
                    Description =
                        "Do wynajęcia Volkswagen Passat B8. Samochód ma około 60 000 przebiegu, jest w stanie dobrym. Kupiony został w Polsce, jest w 100% bezwypadkowy. Wszystkie elementy nadwozia są w oryginalnym lakierze.",
                    CarReservations = new List<CarReservations>()
                    {
                        new CarReservations()
                        {
                            Reservations = new Reservations()
                            {
                                Status = "OK",
                                DateFrom = new DateTime(2022, 12, 12), //format YYYY-MM-DD
                                DateTo = new DateTime(2022, 12, 14), //format YYYY-MM-DD
                                Name = "Oskar",
                                LastName = "Kowalski"
                            },
                        },
                        new CarReservations()
                        {
                            Reservations = new Reservations()
                            {
                                Status = "OK",
                                DateFrom = new DateTime(2022, 12, 15), //format YYYY-MM-DD
                                DateTo = new DateTime(2022, 12, 17), //format YYYY-MM-DD
                                Name = "Oskar",
                                LastName = "Kowalski"
                            },
                        },
                    },

                    CarLocation = new List<CarLocation>()
                    {
                        new CarLocation()
                        {
                            DateTime = new DateTimeOffset(2022, 10, 10, 16, 00, 00,
                                new TimeSpan(0, 0, 0)), //format: YYYY-MM-DD HH:MI:SS
                            Location = "Warsaw",
                            Name = "Oskar",
                            LastName = "Kowalski"
                        },
                        new CarLocation()
                        {
                            DateTime = new DateTimeOffset(2022, 12, 17, 16, 00, 00,
                                new TimeSpan(0, 0, 0)), //format: YYYY-MM-DD HH:MI:SS
                            Location = "Rzeszow",
                            Name = "Oskar",
                            LastName = "Kowwalski"
                        },
                    },
                    Images = new List<Image>()
                    {
                        new Image()
                        {
                            Src =
                                "https://thumbs.img-sprzedajemy.pl/1000x901c/99/a6/91/volkswagen-passat-b8-kombi-data-produkcji-22-diesel-531704361.jpg",
                            Alt = "Passat B8 zdjęcie"
                        },
                        new Image()
                        {
                            Src = "https://bi.im-g.pl/im/d3/14/10/z16860627IER,Volkswagen-Passat-B8.jpg",
                            Alt = "Passat B8 zdjęcie"
                        },
                        new Image()
                        {
                            Src =
                                "https://thumbs.img-sprzedajemy.pl/1000x901c/5e/b8/09/volkswagen-passat-b8-18-tsi-180-comfortline-swiatla-led-warszawa-527498534.jpg",
                            Alt = "Passat B8 zdjęcie"
                        },
                        new Image()
                        {
                            Src =
                                "https://thumbs.img-sprzedajemy.pl/1000x901c/9a/92/84/volkswagen-passat-b8-18-tsi-180-comfortline-centralny-zamek-mazowieckie-warszawa-527498537.jpg",
                            Alt = "Passat B8 zdjęcie"
                        },
                        new Image()
                        {
                            Src = "https://bi.im-g.pl/im/c9/14/10/z16860617AMP,Volkswagen-Passat-B8.jpg",
                            Alt = "Passat B8 zdjęcie"
                        },
                        new Image()
                        {
                            Src =
                                "https://thumbs.img-sprzedajemy.pl/1000x901c/12/de/f2/volkswagen-passat-comfortline-salon-polska-1wl-manualna-wloclawek-555476152.jpg",
                            Alt = "Passat B8 zdjęcie"
                        }
                    },
                    CarEquipments = new List<CarEquipment>()
                    {
                        new CarEquipment()
                        {
                            Name = "Interfejs Bluetooth"
                        },
                        new CarEquipment()
                        {
                            Name = "Zestaw głośnomówiący"
                        },
                        new CarEquipment()
                        {
                            Name = "Gniazdo USB"
                        },
                        new CarEquipment()
                        {
                            Name = "System nawigacji satelitarnej"
                        },
                        new CarEquipment()
                        {
                            Name = "Klimatyzacja automatyczna (dwustrefowa)"
                        },
                        new CarEquipment()
                        {
                            Name = "Podłokietniki"
                        },
                        new CarEquipment()
                        {
                            Name = "Kierownica ze sterowaniem radia"
                        },
                        new CarEquipment()
                        {
                            Name = "Podgrzewane fotele"
                        },
                        new CarEquipment()
                        {
                            Name = "Kierownica wielofunkcyjna"
                        },
                        new CarEquipment()
                        {
                            Name = "Lusterka boczne ustawiane elektrycznie"
                        },
                        new CarEquipment()
                        {
                            Name = "Asystent świateł drogowych"
                        },
                        new CarEquipment()
                        {
                            Name = "System Start/Stop"
                        },
                        new CarEquipment()
                        {
                            Name = "Oświetlenie wnętrza LED"
                        },
                        new CarEquipment()
                        {
                            Name = "ESP"
                        },
                        new CarEquipment()
                        {
                            Name = "Poduszka powietrzna kierowcy"
                        },
                        new CarEquipment()
                        {
                            Name = "Boczna poduszka powietrzna kierowcy"
                        },
                        new CarEquipment()
                        {
                            Name = "System wspomagania hamowania"
                        }
                    }
                },
            },
        };
        return results;
    }
}
