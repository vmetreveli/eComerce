using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Catalog.Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers.v2;

[ApiController]
[Route("api/v2/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;

    private readonly IMediator _mediator;



    public CatalogController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products =  await _mediator.Send(new GetProductsQuery(), cancellationToken);
        if (products.Any()) return Ok(products);
        return NotFound();
    }
    //
    // [HttpGet("{id:length(24)}", Name = "GetProduct")]
    // [ProducesResponseType((int) HttpStatusCode.NotFound)]
    // [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    // public async Task<IActionResult> GetProductById(string id)
    // {
    //     var product = await _repository.GetProduct(id);
    //     if (product == null)
    //     {
    //         _logger.LogError($"Product with id: {id}, not found.");
    //         return NotFound();
    //     }
    //
    //     return Ok(product);
    // }
    //
    // [Route("[action]/{category}", Name = "GetProductByCategory")]
    // [HttpGet]
    // [ProducesResponseType((int) HttpStatusCode.NotFound)]
    // [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    // public async Task<IActionResult> GetProductByCategory(string category)
    // {
    //     var products = await _repository.GetProductByCategory(category);
    //     if (products.Any()) return Ok(products);
    //     return NotFound();
    // }
    //
    // [HttpPost]
    // [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    // public async Task<IActionResult> CreateProduct([FromBody] Product product)
    // {
    //     await _repository.CreateProduct(product);
    //
    //     return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
    // }
    //
    // [HttpPut]
    // [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    // public async Task<IActionResult> UpdateProduct([FromBody] Product product) =>
    //     Ok(await _repository.UpdateProduct(product));
    //
    // [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    // [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    // public async Task<IActionResult> DeleteProductById(string id) =>
    //     Ok(await _repository.DeleteProduct(id));
}