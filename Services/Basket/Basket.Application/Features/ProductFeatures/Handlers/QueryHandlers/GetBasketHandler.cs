using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Abstractions.Messaging;
using Basket.Application.Dto;
using Basket.Application.Features.ProductFeatures.Queries;
using Basket.Domain.Interfaces.Repository;

namespace Basket.Application.Features.ProductFeatures.Handlers.QueryHandlers;

public class GetBasketHandler : IQueryHandler<GetBasketQuery, ShoppingCartDto>
{
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;

    public GetBasketHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }


    public async Task<ShoppingCartDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasket(request.UserName,cancellationToken);

        var res = _mapper.Map<ShoppingCartDto>(basket) ?? new ShoppingCartDto(request.UserName);

        return res;
    }
}