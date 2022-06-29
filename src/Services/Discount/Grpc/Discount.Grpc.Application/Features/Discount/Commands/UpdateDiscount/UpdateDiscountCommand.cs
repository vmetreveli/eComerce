using MediatR;

namespace Discount.Grpc.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateDiscountCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}