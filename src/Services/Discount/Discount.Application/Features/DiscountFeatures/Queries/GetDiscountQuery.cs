using System.Collections.Generic;
using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Dto;

namespace Discount.Application.Features.DiscountFeatures.Queries;

public class GetDiscountQuery : IQuery<CouponDto>
{
    public string ProductName { get; set; }
}