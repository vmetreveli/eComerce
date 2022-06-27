using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.GrpcServices;
using Basket.Application.Dto;
using Basket.Application.Features.Products.Commands.DeleteBasket;
using Basket.Application.Features.Products.Commands.UpdateBasket;
using Basket.Application.Features.Products.Queries.GetBasket;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IMediator mediator, DiscountGrpcService discountGrpcService, IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _discountGrpcService = discountGrpcService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

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
        foreach (var item in shoppingCartDto.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price = coupon.Amount;
        }

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

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Accepted)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutDto basketCheckout,
        CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
        // send checkout event to rabbitmq
        // remove the basket

        // get existing basket with total price
        var basket = await _mediator.Send(new GetBasketQuery {UserName = basketCheckout.UserName}, cancellationToken);
        if (basket == null) return BadRequest();

        // send checkout event to rabbitmq
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish(eventMessage, cancellationToken);

        // remove the basket
        await _mediator.Send(new DeleteBasketCommand {UserName = basket.UserName},
            cancellationToken);


        return Accepted();
    }
}