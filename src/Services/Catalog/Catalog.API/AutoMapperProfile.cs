using AutoMapper;
using Catalog.Application.Dto;
using Catalog.Domain.Models.Entities;

namespace Catalog.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}