using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.Products.Queries.GetBasket;

public class GetBasketQuery : IRequest<ShoppingCartDto>
{
    public string UserName { get; set; }
}