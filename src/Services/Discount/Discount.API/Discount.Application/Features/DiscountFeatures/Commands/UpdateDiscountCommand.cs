using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Dto;

namespace Discount.Application.Features.DiscountFeatures.Commands;

public class UpdateDiscountCommand : ICommand<bool>
{
    public CouponDto CouponDto { get; set; }
}