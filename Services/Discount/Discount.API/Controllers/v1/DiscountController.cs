using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator) =>
        _mediator = mediator;
}