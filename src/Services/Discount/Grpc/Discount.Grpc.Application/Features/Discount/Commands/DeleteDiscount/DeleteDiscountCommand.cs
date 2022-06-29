using MediatR;

namespace Discount.Grpc.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommand : IRequest<bool>
{
    public string ProductName { get; set; }
}