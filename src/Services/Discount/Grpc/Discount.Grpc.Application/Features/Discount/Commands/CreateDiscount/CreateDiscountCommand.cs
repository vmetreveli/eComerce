using MediatR;

namespace Discount.Grpc.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}