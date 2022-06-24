using MediatR;

namespace Basket.Application.Features.Products.Commands.DeleteBasket;

public class DeleteBasketCommand : IRequest<Unit>
{
    public string UserName { get; init; }
}