using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.Application.Contracts.Persistence;
using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.Products.Commands.UpdateBasket;

public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, ShoppingCartDto>
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