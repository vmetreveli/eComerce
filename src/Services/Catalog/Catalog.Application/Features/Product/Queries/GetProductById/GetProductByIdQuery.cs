using Catalog.Application.Features.Product.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.ProductFeatures.Queries;

public class GetProductByIdQuery :  IRequest<ProductVm>
{
    public string Id { get; set; }
}