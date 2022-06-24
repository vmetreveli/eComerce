using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Contracts.Persistence;
using MediatR;

namespace Basket.Application.Features.Products.Commands.BasketCheckout;

public class BasketCheckoutCommandHandler : IRequestHandler<BasketCheckoutCommand, Unit>
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
        await _basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        return Unit.Value;
    }
}