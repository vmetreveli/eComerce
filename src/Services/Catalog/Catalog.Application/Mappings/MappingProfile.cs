using AutoMapper;
using Catalog.Application.Features.Product.Queries.GetProducts;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductVm>().ReverseMap();
    }
}