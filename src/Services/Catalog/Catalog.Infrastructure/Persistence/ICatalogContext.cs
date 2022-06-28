using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}