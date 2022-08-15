using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Contracts.Persistence;
using Basket.Application.Dto;
using Basket.Application.Exceptions;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Application.Features.Products.Commands.BasketCheckout;

public class BasketCheckoutCommandHandler : IRequestHandler<BasketCheckoutCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    private IPublishEndpoint _publishEndpoint;
    public BasketCheckoutCommandHandler(IBasketRepository basketRepository, IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }


    public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set TotalPrice on basketCheckout eventMessage
        // send checkout event to rabbitmq
        // remove the basket

        // get existing basket with total price
        var basket = await _basketRepository.GetBasket(request.BasketCheckoutDto.UserName,cancellationToken);
        if (basket == null)
        {
            throw new NotFoundException(nameof(BasketCheckoutDto), request.BasketCheckoutDto.UserName);
        }

        // send checkout event to rabbitmq
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(request.BasketCheckoutDto);
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

        // remove the basket
        await _basketRepository.DeleteBasket(basket.UserName, cancellationToken);

        return Unit.Value;
    }
}