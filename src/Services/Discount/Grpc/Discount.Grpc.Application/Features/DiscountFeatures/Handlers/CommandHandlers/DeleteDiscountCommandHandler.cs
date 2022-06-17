using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Abstractions.Messaging;
using Discount.Grpc.Application.Features.DiscountFeatures.Commands;
using Discount.Grpc.Domain.Interfaces.Repository;

namespace Discount.Grpc.Application.Features.DiscountFeatures.Handlers.CommandHandlers;

public class DeleteDiscountCommandHandler : ICommandHandler<DeleteDiscountCommand, bool>
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