using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;

namespace Catalog.Application.Features.ProductFeatures.Queries;

public class GetProductByIdQuery : IQuery<ProductDto>
{
    public string Id { get; set; }
}