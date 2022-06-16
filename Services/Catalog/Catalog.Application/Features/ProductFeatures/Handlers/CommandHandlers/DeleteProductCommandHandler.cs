using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Features.ProductFeatures.Commands;
using Catalog.Data.Repositories;
using Catalog.Domain.Interfaces.Repository;

namespace Catalog.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }


    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await _productRepository.DeleteProduct(request.Id, cancellationToken);
}