using Catalog.API.Middleware;
using Catalog.Application;
using Catalog.Application.Behaviors;
using Catalog.Data;
using Catalog.Data.Context;
using Catalog.Data.Repositories;
using Catalog.Domain.Interfaces.Repository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddControllers();

        services.Configure<DatabaseSettings>(
            configuration.GetSection("DatabaseSettings"));

        services.AddScoped<ICatalogContext, CatalogContext>();

        /* Validation */
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddSerilogServices(configuration);


        #region Repositories

        services.AddScoped<IProductRepository, ProductRepository>();

        #endregion
    }
}