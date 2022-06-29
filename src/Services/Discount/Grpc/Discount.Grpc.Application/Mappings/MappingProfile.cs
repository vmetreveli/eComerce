using AutoMapper;
using Discount.Grpc.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Grpc.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Grpc.Application.Features.Discount.Queries.GetDiscount;
using Discount.Grpc.Domain.Entities;

namespace Discount.Grpc.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponVm>().ReverseMap();
        CreateMap<Coupon, CreateDiscountCommand>().ReverseMap();
        CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();
    }
}