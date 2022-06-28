using Catalog.Application.Contracts.Persistence;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace Catalog.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(c=>
            configuration.GetSection("DatabaseSettings"));

        services.AddScoped<ICatalogContext, CatalogContext>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}