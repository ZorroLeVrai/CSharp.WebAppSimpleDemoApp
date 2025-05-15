using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.Models;
using WebAppDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppDemo.Controllers;

[Authorize]
[Route("api/[controller]")]
[Route("api/v{version:apiversion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    // GET: api/<ProductController>
    [HttpGet]
    [MapToApiVersion("1.0")]
    public ActionResult<Dictionary<int, Product>> Get()
    {
        _logger.LogInformation("V1: Récupération de tous les produits");
        return Ok(_productService.GetAllProducts());
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<Dictionary<int, Product>>> GetAsync()
    {
        _logger.LogInformation("V2: Récupération de tous les produits (asynchrone)");
        return Ok(await _productService.GetAllProductsAsync());
    }

    //GET api/<ProductController>/5
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        _logger.LogInformation("Récupération du produit {id}", id);
        if (_productService.TryGetProduct(id, out Product? product))
        {
            return Ok(product);
        }

        return NotFound();
    }

    // POST api/<ProductController>
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newProductId = _productService.AddProduct(product);

        return CreatedAtAction(nameof(Get), new { id = newProductId }, product);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public IActionResult Put(int id, [FromBody] Product product)
    {
        _logger.LogInformation("V1: Mise à jour du produit {id}", id);
        if (_productService.UpdateProduct(id, product))
        {
            return NoContent();
        }

        return NotFound();
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Product product)
    {
        _logger.LogInformation("V2: Mise à jour du produit {id}", id);
        if (await _productService.UpdateProductAsync(id, product))
        {
            return NoContent();
        }

        return NotFound();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _productService.DeleteProduct(id);
        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}
