using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;
using Catalog.Application.Features.ProductFeatures.Queries;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Interfaces.Repository;

namespace Catalog.Application.Features.ProductFeatures.Handlers.QueryHandlers;

public class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, IEnumerable<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByCategory(request.Category, cancellationToken);

        if (!product.Any()) throw new ProductNotFoundException(request.Category);

        return product.Select(i => _mapper.Map<ProductDto>(i));
    }
}