using System;
using Basket.API.GrpcServices;
using Basket.API.Middleware;
using Basket.Application;
using Basket.Infrastructure;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Basket.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Basket.API", Version = "v1"});
        });

        services.AddTransient<ExceptionHandlingMiddleware>();


        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
        {
            o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]);
        });

        services.AddScoped<DiscountGrpcService>();
    }
}