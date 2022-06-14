using AutoMapper;
using Basket.API.Entities;
using Basket.Application.Dto;

namespace Basket.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ShoppingCartDto, ShoppingCart>();
        CreateMap<ShoppingCart, ShoppingCartDto>();
    }
}