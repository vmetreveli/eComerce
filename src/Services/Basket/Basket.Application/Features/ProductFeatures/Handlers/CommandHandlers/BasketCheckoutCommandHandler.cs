using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Abstractions.Messaging;
using Basket.Application.Features.ProductFeatures.Commands;
using Basket.Domain.Interfaces.Repository;
using MediatR;

namespace Basket.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class BasketCheckoutCommandHandler : ICommandHandler<BasketCheckoutCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketCheckoutCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }


    public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
    {
       // await _basketRepository.DeleteBasket(request.BasketCheckout, cancellationToken);
        return Unit.Value;
    }
}