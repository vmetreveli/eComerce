using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Interfaces.Repository;
using Catalog.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;

    //TODO
    //Need refactoring
    private readonly IProductRepository _repository;

    public CatalogController(ILogger<CatalogController> logger, IProductRepository repository)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _repository = repository ?? throw new ArgumentException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _repository.GetProducts(cancellationToken);
        if (products.Any()) return Ok(products);
        return NotFound();
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductById(string id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProduct(id, cancellationToken);
        if (product == null)
        {
            _logger.LogError($"Product with id: {id}, not found.");
            return NotFound();
        }

        return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductByCategory(string category, CancellationToken cancellationToken)
    {
        var products = await _repository.GetProductByCategory(category, cancellationToken);
        if (products.Any()) return Ok(products);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product, CancellationToken cancellationToken)
    {
        await _repository.CreateProduct(product, cancellationToken);

        return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product, CancellationToken cancellationToken) =>
        Ok(await _repository.UpdateProduct(product, cancellationToken));

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id, CancellationToken cancellationToken) =>
        Ok(await _repository.DeleteProduct(id, cancellationToken));
}