using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Dto;
using Discount.Grpc.Application.Features.DiscountFeatures.Queries;
using Discount.Grpc.Domain.Exceptions;
using Discount.Grpc.Domain.Interfaces.Repository;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.QueryHandlers;

public class GetDiscountQueryHandler : IQueryHandler<GetDiscountQuery, CouponDto>
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }


    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetDiscount(request.ProductName,cancellationToken);

        if (discount == null)
        {
            throw new DiscountNotFoundException(request.ProductName);
        }

        return _mapper.Map<CouponDto>(discount);
    }
}