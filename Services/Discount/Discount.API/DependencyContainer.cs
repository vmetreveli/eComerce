using System.Reflection.Metadata;
using Catalog.API;
using Discount.API.Middleware;
using Discount.Application.Behaviors;
using Discount.Data.Repositories;
using Discount.Domain.Interfaces.Repository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Discount.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Discount.API", Version = "v1"});
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