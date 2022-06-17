using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Dto;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Queries;

public class GetDiscountQuery : IQuery<CouponDto>
{
    public string ProductName { get; set; }
}