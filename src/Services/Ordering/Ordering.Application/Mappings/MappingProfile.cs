using System.Reflection;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderVm>().ReverseMap();
        CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
    }

}