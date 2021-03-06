using Catalog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
        var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);

        Products = database.GetCollection<Product>(configuration.GetSection("DatabaseSettings:CollectionName").Value);
        CatalogContextSeed.SeedData(Products);
    }

    // public CatalogContext(IOptions<DatabaseSettings> dbOptions)
    // {
    //     var settings = dbOptions.Value;
    //     var client =
    //         new MongoClient(settings
    //             .ConnectionString); //configuration.GetSection("DatabaseSettings:ConnectionString").Value);
    //     var database =
    //         client.GetDatabase(settings
    //             .DatabaseName); //configuration.GetSection("DatabaseSettings:DatabaseName").Value);
    //
    //     Products = database.GetCollection<Product>(settings
    //         .CollectionName); //configuration.GetSection("DatabaseSettings:CollectionName").Value);
    //     CatalogContextSeed.SeedData(Products);
    // }


    public IMongoCollection<Product> Products { get; }
}