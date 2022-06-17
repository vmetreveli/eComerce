using AutoMapper;
using Discount.Application.Dto;
using Discount.Domain.Models.Entities;

namespace Discount.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();
    }
}