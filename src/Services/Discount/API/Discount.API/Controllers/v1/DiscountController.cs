using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Application.Features.Discount.Commands.DeleteDiscount;
using Discount.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Application.Features.Discount.Queries.GetDiscount;
using Discount.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator) =>
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetDiscount(string productName, CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(
            new GetDiscountQuery {ProductName = productName}, cancellationToken));


    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand coupon,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            coupon, cancellationToken);
        return CreatedAtRoute("GetDiscount", new {productName = coupon.ProductName}, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(CouponVm), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] UpdateDiscountCommand coupon,
        CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(
            coupon, cancellationToken));


    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteDiscount(string productName, CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(
            new DeleteDiscountCommand {ProductName = productName}, cancellationToken));
}