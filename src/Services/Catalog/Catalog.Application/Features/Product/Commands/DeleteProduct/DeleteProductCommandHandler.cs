using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using MediatR;

namespace Catalog.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
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