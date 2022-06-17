using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Dto;
using Discount.Grpc.Application.Features.DiscountFeatures.Queries;
using Discount.Grpc.Domain.Interfaces.Repository;
using Grpc.Core;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.QueryHandlers;

public class GetDiscountQueryHandler : IQueryHandler<GetDiscountQuery, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }


    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetDiscount(request.ProductName, cancellationToken);

        if (discount == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount With ProductName={request.ProductName} " +
            "is not found."));

        return _mapper.Map<CouponDto>(discount);
    }
}