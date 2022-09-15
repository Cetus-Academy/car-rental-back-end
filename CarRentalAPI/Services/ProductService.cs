using CarRentalAPI.Entities;
using CarRentalAPI.DAL;

namespace CarRentalAPI.Services
{
    public interface IProductService
    {
        bool Delete(int id);
        bool Update(int id, Product product);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetSearched(string searchString);
        Product GetById(int id);
        Product GetBySlug(string slug);
        int Create(Product product);
    }
    public class ProductService : IProductService
    {
        private readonly CarDbContext _dbContext;

        public ProductService(CarDbContext dbContext)//, IMapper mapper
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }
        
        public bool Delete(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product is null) return false;

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return true;
        }
        public bool Update(int id, Product product)
        {
            var foundProduct = _dbContext
                .Products
                .FirstOrDefault(r => r.Id == id);

            if (foundProduct is null) return false;

            foundProduct.Slug = product.Slug;
            foundProduct.Description = product.Description;
            //TODO: ask what rows can be changed

            _dbContext.SaveChanges();

            return true;
        }
        public IEnumerable<Product> GetAll()
        {
            var products = _dbContext
                .Products
                .ToList();

            return products;
        }
        public IEnumerable<Product> GetSearched(string searchString)
        {
            var products = _dbContext
                .Products
                .Where(p => p.Name.Contains(searchString))//Name!
                .ToArray();
            
            return products;
        }
        public Product GetById(int id)
        {
            var product = _dbContext
                .Products
                .FirstOrDefault(r => r.Id == id);

            return product;
        }
        public Product GetBySlug(string slug)
        {
            var product = _dbContext
                .Products
                .FirstOrDefault(r => r.Slug == slug);

            return product;
        }
        public int Create(Product product)
        {
            //var car = _mapper.Map<Car>(dto);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return product.Id;
        }
    }
}
