using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _productRepository.GetProducts());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var product = await _productRepository.GetProduct(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
    {
        var products = await _productRepository.GetProductsByCategory(category);
        if (products == null)
            return NotFound();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        return Ok(await _productRepository.AddProduct(product));
        // return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
    {
        return Ok(await _productRepository.UpdateProduct(product));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProduct(string id)
    {
        return Ok(await _productRepository.DeleteProduct(id));
    }
}