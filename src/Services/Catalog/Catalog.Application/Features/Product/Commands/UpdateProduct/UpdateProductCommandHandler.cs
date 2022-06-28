using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using MediatR;

namespace Catalog.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }


    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _productRepository.GetProductAsync(request.Id, cancellationToken);
        if (productToUpdate == null) throw new NotFoundException(nameof(Product), request.Id);

        _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Domain.Entities.Product));

        return await _productRepository.UpdateProductAsync(productToUpdate, cancellationToken);
    }
}