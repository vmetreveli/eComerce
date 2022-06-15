using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Discount.Application.Features.DiscountFeatures.Commands;
using Discount.Domain.Interfaces.Repository;

namespace Discount.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class DeleteDiscountCommandHandler : ICommandHandler<DeleteDiscountCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }


    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken) =>
        await _discountRepository.DeleteDiscount(request.ProductName, cancellationToken);
}