using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Application.Features.Product.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Product.Queries.GetProductByCategory;

public class GetProductByCategoryQueryHandler : IRequestHandler<GetProductByCategoryQuery, IEnumerable<ProductVm>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByCategoryQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVm>> Handle(GetProductByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByCategory(request.Category, cancellationToken);

        if (!product.Any()) //throw new NotFoundException(request.Category);
            throw new NotFoundException(nameof(Product), request.Category);

        return product.Select(i => _mapper.Map<ProductVm>(i));
    }
}