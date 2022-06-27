using AutoMapper;
using Basket.API.Entities;
using Basket.Application.Dto;
using EventBus.Messages.Events;

namespace Basket.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();

        CreateMap<ShoppingCartItemDto, ShoppingCartItem>().ReverseMap();

        CreateMap<BasketCheckoutDto, BasketCheckoutEvent>().ReverseMap();
    }
}