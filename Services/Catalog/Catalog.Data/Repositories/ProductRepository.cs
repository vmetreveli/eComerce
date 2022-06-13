using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Data.Context;
using Catalog.Domain.Interfaces.Repository;
using Catalog.Domain.Models.Entities;
using MongoDB.Driver;

namespace Catalog.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken) =>
        await _context
            .Products
            .Find(p => true)
            .ToListAsync(cancellationToken);

    public async Task<Product> GetProduct(string id,CancellationToken cancellationToken) =>
        await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName,CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateProduct(Product product, CancellationToken cancellationToken) =>
        await _context.Products.InsertOneAsync(product, cancellationToken);

    public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        var updateResult = await _context
            .Products
            .ReplaceOneAsync(g => g.Id == product.Id, product, cancellationToken: cancellationToken);

        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var deleteResult = await _context
            .Products
            .DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;
    }
}