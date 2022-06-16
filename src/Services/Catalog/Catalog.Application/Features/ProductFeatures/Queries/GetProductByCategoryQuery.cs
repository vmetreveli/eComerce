using System.Collections.Generic;
using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;

namespace Catalog.Application.Features.ProductFeatures.Queries;

public class GetProductByCategoryQuery : IQuery<IEnumerable<ProductDto>>
{
    public string Category { get; set; }
}