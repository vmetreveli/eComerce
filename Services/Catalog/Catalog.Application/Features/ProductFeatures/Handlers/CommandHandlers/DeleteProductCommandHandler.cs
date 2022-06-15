using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Features.ProductFeatures.Commands;
using Catalog.Data.Repositories;

namespace Catalog.Application.Features.ProductFeatures.Handlers.CommandHandlers;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly ProductRepository _productRepository;

    public DeleteProductCommandHandler(IMapper mapper, ProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }


    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await _productRepository.DeleteProduct(request.Id, cancellationToken);
}