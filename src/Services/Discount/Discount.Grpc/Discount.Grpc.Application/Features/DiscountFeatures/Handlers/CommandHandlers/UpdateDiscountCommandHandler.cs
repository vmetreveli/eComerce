using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Features.DiscountFeatures.Commands;
using Discount.Grpc.Domain.Interfaces.Repository;
using Discount.Grpc.Domain.Models.Entities;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class UpdateProductCommandHandler : ICommandHandler<UpdateDiscountCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public UpdateProductCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }


    public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponDto);
        return await _discountRepository.UpdateDiscount(coupon, cancellationToken);
    }
}