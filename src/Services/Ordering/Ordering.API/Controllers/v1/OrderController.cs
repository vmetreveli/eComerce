using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) =>
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{userName}", Name = "GetOrder")]
    [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrdersByUserName(string userName,
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersListQuery(userName);
        var orders = await _mediator.Send(query, cancellationToken);
        return Ok(orders);
    }

    // testing purpose
    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand() {Id = id};
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}