using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discount.Domain.Interfaces.Repository;
using Discount.Domain.Models.Entities;

namespace Discount.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Product> GetProduct(string id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task CreateProduct(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<bool> DeleteProduct(string id, CancellationToken cancellationToken) => throw new NotImplementedException();
}