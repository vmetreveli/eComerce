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
using Microsoft.OpenApi.Models;

namespace Catalog.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(Catalog.Application.AssemblyReference).Assembly);

        services.AddControllers();

        services.Configure<DatabaseSettings>(
            configuration.GetSection("DatabaseSettings"));

        services.AddScoped<ICatalogContext, CatalogContext>();


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Catalog.API", Version = "v1"});
        });

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