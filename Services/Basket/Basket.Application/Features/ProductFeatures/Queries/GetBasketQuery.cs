using System.Collections.Generic;
using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;

namespace Basket.Application.Features.ProductFeatures.Queries;

public class GetBasketQuery : IQuery<ShoppingCartDto>
{
    public string UserName { get; set; }
}