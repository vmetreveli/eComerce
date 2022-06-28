using AutoMapper;
using Discount.Application.Features.Discount.Commands.CreateDiscount;
using Discount.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Application.Features.Discount.Queries.GetDiscount;
using Discount.Domain.Models.Entities;

namespace Discount.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponVm>().ReverseMap();
        CreateMap<Coupon, CreateDiscountCommand>().ReverseMap();
        CreateMap<Coupon, UpdateDiscountCommand>().ReverseMap();
    }
}