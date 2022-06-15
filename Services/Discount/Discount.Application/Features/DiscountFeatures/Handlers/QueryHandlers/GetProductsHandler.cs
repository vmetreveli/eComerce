using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;
using Catalog.Application.Features.ProductFeatures.Queries;
using Discount.Domain.Interfaces.Repository;

namespace Discount.Application.Features.DiscountFeatures.Handlers.QueryHandlers;

public class GetProductsHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetProductsHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}