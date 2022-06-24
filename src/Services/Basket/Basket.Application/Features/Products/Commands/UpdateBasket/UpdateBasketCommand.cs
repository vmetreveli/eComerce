using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.Products.Commands.UpdateBasket;

public class UpdateBasketCommand : IRequest<ShoppingCartDto>
{
    public ShoppingCartDto ShoppingCartDto { get; init; }
}