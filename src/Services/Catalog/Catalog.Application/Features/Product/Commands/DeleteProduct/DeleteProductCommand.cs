using MediatR;

namespace Catalog.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<bool>
{
    public string Id { get; set; }
}