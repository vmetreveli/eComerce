using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Features.ProductFeatures.Commands;
using Catalog.Data.Repositories;
using Catalog.Domain.Interfaces.Repository;
using Catalog.Domain.Models.Entities;
using MediatR;

namespace Catalog.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }


    [Obsolete("Obsolete")]
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductDto);
        await _productRepository.CreateProduct(product, cancellationToken);
        return Unit.Value;
    }
}