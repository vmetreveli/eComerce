using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;
using Catalog.Application.Features.ProductFeatures.Queries;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Interfaces.Repository;

namespace Catalog.Application.Features.ProductFeatures.Handlers.QueryHandlers;

public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProduct(request.Id, cancellationToken);

        if (product == null) throw new ProductNotFoundException(request.Id);

        return _mapper.Map<ProductDto>(product);
        ;
    }
}