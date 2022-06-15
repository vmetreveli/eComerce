using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Features.ProductFeatures.Commands;
using Catalog.Data.Repositories;
using Catalog.Domain.Models.Entities;

namespace Catalog.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly ProductRepository _productRepository;

    public UpdateProductCommandHandler(IMapper mapper, ProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }


    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductDto);
        return await _productRepository.UpdateProduct(product, cancellationToken);
    }
}