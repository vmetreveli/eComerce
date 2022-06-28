using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistence;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    Task<Product> GetProductAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken);

    Task CreateProduct(Product product, CancellationToken cancellationToken);
    Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteProduct(string id, CancellationToken cancellationToken);
}