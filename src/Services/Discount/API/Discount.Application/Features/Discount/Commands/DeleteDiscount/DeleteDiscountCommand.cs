using MediatR;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommand : IRequest<bool>
{
    public string ProductName { get; set; }
}