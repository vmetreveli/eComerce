using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using MediatR;

namespace Catalog.Application.Features.Product.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductVm>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<List<ProductVm>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts(cancellationToken);

        var res = products.Select(i => _mapper.Map<ProductVm>(i))
            .ToList();

        if (!res.Any())
            // throw new ProductNotFoundException(string.Empty);
            throw new NotFoundException(nameof(Product), request);

        return res;
    }
}