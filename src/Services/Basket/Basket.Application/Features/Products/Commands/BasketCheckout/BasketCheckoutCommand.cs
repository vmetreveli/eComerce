using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.Products.Commands.BasketCheckout;

public class BasketCheckoutCommand: IRequest<Unit>
{
    public BasketCheckoutDto BasketCheckoutDto { get; init; }
}
