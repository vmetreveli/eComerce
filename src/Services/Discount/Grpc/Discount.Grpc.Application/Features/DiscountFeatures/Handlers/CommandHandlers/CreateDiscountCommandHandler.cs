using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Features.DiscountFeatures.Commands;
using Discount.Grpc.Domain.Interfaces.Repository;
using Discount.Grpc.Domain.Models.Entities;
using MediatR;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class CreateDiscountCommandHandler : ICommandHandler<CreateDiscountCommand, Unit>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public CreateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }


    public async Task<Unit> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _mapper.Map<Coupon>(request.CouponDto);
        await _discountRepository.CreateDiscount(coupon, cancellationToken);
        return Unit.Value;
    }
}