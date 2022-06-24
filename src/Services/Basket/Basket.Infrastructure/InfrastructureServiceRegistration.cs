using System.Reflection;
using Basket.Application.Contracts.Persistence;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Basket.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // MassTransit-RabbitMQ Configuration
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx,cfg)=>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
            });

        });

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = $"{typeof(Assembly).Assembly}_";
        });

        return services;
    }
}