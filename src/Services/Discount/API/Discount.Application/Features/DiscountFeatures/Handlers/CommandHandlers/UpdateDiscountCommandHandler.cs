using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Features.DiscountFeatures.Commands;
using Discount.Domain.Interfaces.Repository;
using Discount.Domain.Models.Entities;

namespace Discount.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class UpdateProductCommandHandler : ICommandHandler<UpdateDiscountCommand, bool>
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
        var coupon = _mapper.Map<Coupon>(request.CouponDto);
        return await _discountRepository.UpdateDiscount(coupon, cancellationToken);
    }
}