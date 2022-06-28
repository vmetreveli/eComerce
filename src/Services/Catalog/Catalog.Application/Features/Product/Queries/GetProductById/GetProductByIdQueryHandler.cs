using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Application.Features.Product.Queries.GetProducts;
using Catalog.Application.Features.ProductFeatures.Queries;
using MediatR;

namespace Catalog.Application.Features.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductAsync(request.Id, cancellationToken);

        if (product == null)
            //  throw new ProductNotFoundException(request.Id);
            throw new NotFoundException(nameof(Product), request.Id);


        return _mapper.Map<ProductVm>(product);
        ;
    }
}