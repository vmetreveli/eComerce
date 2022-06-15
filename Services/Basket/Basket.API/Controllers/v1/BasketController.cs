using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.Dto;
using Basket.Application.Features.ProductFeatures.Commands;
using Basket.Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator) =>
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetBasketQuery {UserName = userName}, cancellationToken);

        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCartDto shoppingCartDto,
        CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new UpdateBasketCommand {ShoppingCartDto = shoppingCartDto},
            cancellationToken);

        return Ok(products);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBasketCommand {UserName = userName},
            cancellationToken);

        return Ok();
    }
}