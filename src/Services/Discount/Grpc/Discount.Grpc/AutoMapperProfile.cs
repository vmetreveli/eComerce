using AutoMapper;
using Discount.Grpc.Application.Features.Discount.Queries.GetDiscount;
using Discount.Grpc.Protos;

namespace Discount.Grpc;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<CouponDto, Coupon>();
        // CreateMap<Coupon, CouponDto>();
        // CreateMap<CouponDto, CouponModel>();
        CreateMap<CouponModel, CouponVm>().ReverseMap();
    }
}