using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Contracts.Persistence;
using MediatR;

namespace Basket.Application.Features.Products.Commands.DeleteBasket;

public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }


    public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.UserName, cancellationToken);
        return Unit.Value;
    }
}