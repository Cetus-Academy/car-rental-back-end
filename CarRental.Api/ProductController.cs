using CarRental.Application.Requests;
using CarRental.Domain;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api;

[ApiController]
[Route("products")]
public class CarProductController : ControllerBase
{
    private readonly IProductService _productService;

    public CarProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _productService.Delete(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] Product product, [FromRoute] int id)
    {
        await _productService.Update(id, product);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById([FromRoute] int id)
    {
        var product = await _productService.GetById(id);

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<Product>> GetBySearched([FromQuery] SearchRequest request)
    {
        var products = await _productService.GetSearched(request.searchString);

        return Ok(products);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<Product>> Get([FromRoute] string slug)
    {
        var product = await _productService.GetBySlug(slug);

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product product)
    {
        var id = await _productService.Create(product);

        return Created($"/api/product/{id}", null);
    }
}
