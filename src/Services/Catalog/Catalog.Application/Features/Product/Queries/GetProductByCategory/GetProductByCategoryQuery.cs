using System.Collections.Generic;
using Catalog.Application.Features.Product.Queries.GetProducts;
using MediatR;

namespace Catalog.Application.Features.Product.Queries.GetProductByCategory;

public class GetProductByCategoryQuery :  IRequest<IEnumerable<ProductVm>>
{
    public string Category { get; set; }
}