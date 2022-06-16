using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Dto;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Commands;

public class UpdateDiscountCommand : ICommand<bool>
{
    public CouponDto CouponDto { get; set; }
}