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
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts(cancellationToken);

        var res = products.Select(i => _mapper.Map<ProductDto>(i));

        // if (!res.Any())
        //     throw new CityNotFoundException(request.Name);

        return res;
    }
}