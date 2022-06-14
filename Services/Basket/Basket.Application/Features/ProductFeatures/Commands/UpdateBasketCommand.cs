using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.ProductFeatures.Commands;

public class UpdateBasketCommand: ICommand<ShoppingCartDto>
{
    public ShoppingCartDto ShoppingCartDto { get; init; }
}