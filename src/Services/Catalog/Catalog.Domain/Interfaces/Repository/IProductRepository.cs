using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Models.Entities;

namespace Catalog.Domain.Interfaces.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    Task<Product> GetProduct(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken);

    Task CreateProduct(Product product, CancellationToken cancellationToken);
    Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteProduct(string id, CancellationToken cancellationToken);
}