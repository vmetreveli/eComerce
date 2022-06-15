using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Dto;
using Catalog.Application.Features.ProductFeatures.Commands;
using Catalog.Application.Features.ProductFeatures.Queries;
using MediatR;
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
    private readonly IMediator _mediator;

    public CatalogController(ILogger<CatalogController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductById(string id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductByIdQuery {Id = id}, cancellationToken);
        return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductByCategory(string category, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(
            new GetProductByCategoryQuery {Category = category}, cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto product, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(
            new CreateProductCommand {ProductDto = product}, cancellationToken);

        return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
    }


    [HttpPut]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult>
        UpdateProduct([FromBody] ProductDto product, CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(
            new UpdateProductCommand {ProductDto = product}, cancellationToken));


    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id, CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(
            new DeleteProductCommand {Id = id}, cancellationToken));
}