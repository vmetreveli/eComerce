using Catalog.Application.Abstractions.Messaging;

namespace Discount.Application.Features.DiscountFeatures.Commands;

public class DeleteDiscountCommand : ICommand<bool>
{
    public string ProductName { get; set; }
}