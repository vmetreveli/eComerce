using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.ProductFeatures.Commands;

public class BasketCheckoutCommand: ICommand<Unit>
{
    public BasketCheckoutDto BasketCheckoutDto { get; init; }
}
