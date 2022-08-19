using CarRentalAPI.Entities;
using CarRentalAPI.DAL;

namespace CarRentalAPI.Services
{
    public interface ICarService
    {
        bool Delete(int id);
        bool Update(int id, Car car);
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        Car GetBySlug(string slug);
        int Create(Car car);
    }
    public class CarService : ICarService
    {
        private readonly CarDbContext _dbContext;

        public CarService(CarDbContext dbContext)//, IMapper mapper
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }

        private static readonly string[] brands = new[]
        {
            "Audi", "Ford", "Mazda", "Mercedes-Benz", "Opel", "Porsche", "Škoda", "Volkswagen"
        };
        private static readonly string[] fuelTypes = new[]
        {
            "benzyna", "diesel", "LPG"
        };
        private static readonly string[] priceCategory = new[]
        {
            "Basic", "Standard", "Medium", "Premium"
        };
        public bool Delete(int id)
        {
            var car = _dbContext.Cars.Find(id);

            if (car is null) return false;

            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();

            return true;
        }
        public bool Update(int id, Car car)
        {
            var cars = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == id);

            if (car is null) return false;

            car.Slug = car.Slug;
            car.Description = car.Description;
            //TODO: ask what rows can be changed

            _dbContext.SaveChanges();

            return true;
        }
        public IEnumerable<Car> GetAll()
        {
            var cars = _dbContext
                .Cars
                .ToList();

            return cars;
        }
        public Car GetById(int id)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Id == id);

            return car;
        }
        public Car GetBySlug(string slug)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(r => r.Slug == slug);

            return car;
        }
        public int Create(Car car)
        {
            //var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return car.Id;
        }
    }
}
