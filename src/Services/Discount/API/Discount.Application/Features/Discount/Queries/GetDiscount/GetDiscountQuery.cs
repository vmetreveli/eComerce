using MediatR;

namespace Discount.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQuery : IRequest<CouponVm>
{
    public string ProductName { get; set; }
}