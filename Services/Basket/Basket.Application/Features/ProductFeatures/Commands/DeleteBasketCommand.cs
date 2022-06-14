using System.Threading.Tasks;
using Basket.Application.Abstractions.Messaging;
using MediatR;

namespace Basket.Application.Features.ProductFeatures.Commands;

public class DeleteBasketCommand: ICommand<Unit>
{
    public string UserName { get; init; }
}