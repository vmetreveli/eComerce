using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Dto;
using MediatR;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Commands;

public class CreateDiscountCommand : ICommand<Unit>
{
    public CouponDto CouponDto { get; set; }
}