using Basket.Data.Repositories;
using Basket.Domain.Interfaces.Repository;
using Catalog.Application;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Basket.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = $"{typeof(Startup).Assembly}_";
        });

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Basket.API", Version = "v1"});
        });

        #region Repositories

        services.AddScoped<IBasketRepository, BasketRepository>();

        #endregion
    }
}