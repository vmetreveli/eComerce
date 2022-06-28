using System.Collections.Generic;
using MediatR;

namespace Catalog.Application.Features.Product.Queries.GetProducts;

public class GetProductsQuery : IRequest<List<ProductVm>>
{
}