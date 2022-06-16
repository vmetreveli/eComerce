using Discount.Grpc.Application.Abstractions.Messaging;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Commands;

public class DeleteDiscountCommand : ICommand<bool>
{
    public string ProductName { get; set; }
}