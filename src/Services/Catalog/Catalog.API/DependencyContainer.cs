using Catalog.API.Middleware;
using Catalog.Application;
using Catalog.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Catalog.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddAutoMapper(typeof(Startup));

        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        services.AddControllers();


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Catalog.API", Version = "v1"});
        });

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddSerilogServices(configuration);
    }
}