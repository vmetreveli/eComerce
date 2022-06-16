using Catalog.Domain.Models.Entities;
using MongoDB.Driver;

namespace Catalog.Data.Context;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}