using AutoMapper;
using Discount.Grpc.Application.Dto;
using Discount.Grpc.Domain.Models.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();
        CreateMap<CouponDto, CouponModel>();
        CreateMap<CouponModel, CouponDto>();
    }
}