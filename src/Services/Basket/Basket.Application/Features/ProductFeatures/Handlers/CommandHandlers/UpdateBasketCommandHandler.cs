using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;
using Basket.Application.Features.ProductFeatures.Commands;
using Basket.Domain.Interfaces.Repository;

namespace Basket.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class UpdateBasketCommandHandler : ICommandHandler<UpdateBasketCommand, ShoppingCartDto>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<ShoppingCartDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(request.ShoppingCartDto);
        var res = await _basketRepository.UpdateBasket(shoppingCart, cancellationToken);

        return _mapper.Map<ShoppingCartDto>(res);
    }
}