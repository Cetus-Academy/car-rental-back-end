using CarRental.Application.Exceptions;
using CarRental.Domain;
using CarRental.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Services;

public interface IProductService
{
    Task Delete(int id);
    Task Update(int id, Product product);
    Task<IEnumerable<Product>> GetAll();
    Task<IEnumerable<Product>> GetSearched(string searchString);
    Task<Product> GetById(int id);
    Task<Product> GetBySlug(string slug);
    Task<int> Create(Product product);
}

public class ProductService : IProductService
{
    private readonly CarDbContext _dbContext;

    public ProductService(CarDbContext dbContext) //, IMapper mapper
    {
        _dbContext = dbContext;
        //_mapper = mapper;
    }

    public async Task Delete(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);

        if (product is null)
            throw new NotFoundException("product not found");

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(int id, Product product)
    {
        var foundProduct = await _dbContext
            .Products
            .FirstOrDefaultAsync(r => r.Id == id);

        if (foundProduct is null)
            throw new NotFoundException("no products found");

        foundProduct.Slug = product.Slug;
        foundProduct.Description = product.Description;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await _dbContext
            .Products
            .ToListAsync();

        return products;
    }

    public async Task<IEnumerable<Product>> GetSearched(string searchString)
    {
        var products = await _dbContext
            .Products
            .Where(p => p.Name.Contains(searchString)) //Name!
            .ToListAsync();

        return products;
    }

    public async Task<Product> GetById(int id)
    {
        var product = await _dbContext
            .Products
            .FirstOrDefaultAsync(r => r.Id == id);

        if (product is null) throw new NotFoundException("product not found");

        return product;
    }

    public async Task<Product> GetBySlug(string slug)
    {
        var product = await _dbContext
            .Products
            .FirstOrDefaultAsync(r => r.Slug == slug);

        if (product is null) throw new NotFoundException("product not found");

        return product;
    }

    public async Task<int> Create(Product product)
    {
        //var car = _mapper.Map<Car>(dto);
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return product.Id;
    }
}
