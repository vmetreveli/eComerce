using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Domain.Interfaces.Repository;
using Discount.Domain.Models.Entities;
using MediatR;

namespace Discount.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Unit>
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
        var coupon = _mapper.Map<Coupon>(request);
        await _discountRepository.CreateDiscount(coupon, cancellationToken);
        return Unit.Value;
    }
}