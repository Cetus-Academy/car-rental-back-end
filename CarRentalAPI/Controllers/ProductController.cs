using CarRentalAPI.Entities;
using CarRentalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _productService.Delete(id);

        return isDeleted ? NoContent() : NotFound();
    }
    [HttpPut("{id}")]
    public ActionResult Update([FromBody] Product product, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isUpdated = _productService.Update(id, product);
 
        return isUpdated ? Ok() : NotFound();
    }
    /*[HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        var products = _productService.GetAll();

        return Ok(products);
    }*/
    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById([FromRoute] int id)
    {
        var product = _productService.GetById(id);
        
        return product is null? NotFound() : Ok(product);
    }
    //:regex(^A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ 0-9$)
    //[HttpGet("searchString/{searchString}")]//:regex(^A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ 0-9$)
    //[HttpGet("{searchString=\"\"}")]//:regex(^A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ 0-9$)
    [HttpGet]
    public ActionResult<Product> GetBySearched([FromQuery] string searchString)
    {
        var products = searchString is null ? 
             _productService.GetAll() : 
             _productService.GetSearched(searchString);
        return Ok(products);
    }
    [HttpGet("{slug}")]
    public ActionResult<Product> Get([FromRoute] string slug)
    {
        var product = _productService.GetBySlug(slug);

        return product is null? NotFound() : Ok(product);
    }
    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int id = _productService.Create(product);

        return Created($"/api/product/{id}", null);
    }

}