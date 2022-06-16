using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;

namespace Basket.Application.Features.ProductFeatures.Commands;

public class UpdateBasketCommand : ICommand<ShoppingCartDto>
{
    public ShoppingCartDto ShoppingCartDto { get; init; }
}