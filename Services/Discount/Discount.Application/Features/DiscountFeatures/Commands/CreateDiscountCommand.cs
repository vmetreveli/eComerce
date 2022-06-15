using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Dto;
using MediatR;

namespace Discount.Application.Features.DiscountFeatures.Commands;

public class CreateDiscountCommand : ICommand<Unit>
{
    public CouponDto CouponDto { get; set; }
}