using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Contracts.Infrastructure;
using Discount.Grpc.Application.Exceptions;
using MediatR;

namespace Discount.Grpc.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponVm>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }


    public async Task<CouponVm> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetDiscount(request.ProductName, cancellationToken);

        if (discount == null) throw new NotFoundException(request.ProductName, request);

        return _mapper.Map<CouponVm>(discount);
    }
}