using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Contracts.Persistence;
using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Features.Products.Queries.GetBasket;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, ShoppingCartDto>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }


    public async Task<ShoppingCartDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasket(request.UserName, cancellationToken);

        var res = _mapper.Map<ShoppingCartDto>(basket) ?? new ShoppingCartDto(request.UserName);

        return res;
    }
}