using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public GetProductsHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}