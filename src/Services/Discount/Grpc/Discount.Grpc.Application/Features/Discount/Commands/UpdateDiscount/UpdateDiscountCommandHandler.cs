using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Contracts.Infrastructure;
using Discount.Grpc.Domain.Entities;
using MediatR;

namespace Discount.Grpc.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateProductCommandHandler : IRequestHandler<UpdateDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }


    public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request);
        return await _discountRepository.UpdateDiscount(coupon, cancellationToken);
    }
}