using AutoMapper;
using Catalog.API.Entities;
using Catalog.Application.Dto;

namespace Catalog.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();


    }
}