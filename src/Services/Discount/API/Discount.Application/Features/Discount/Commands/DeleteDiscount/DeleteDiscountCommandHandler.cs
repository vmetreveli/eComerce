using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Domain.Interfaces.Repository;
using MediatR;

namespace Discount.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DeleteDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }


    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken) =>
        await _discountRepository.DeleteDiscount(request.ProductName, cancellationToken);
}