using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Features.DiscountFeatures.Commands;
using Discount.Domain.Interfaces.Repository;
using Discount.Grpc.Domain.Models.Entities;
using MediatR;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class CreateDiscountCommandHandler : ICommandHandler<CreateDiscountCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

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