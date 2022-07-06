using Catalog.API.Middleware;
using Catalog.Application;
using Catalog.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration.GetValue<string>("IdentityProviderBaseUrl");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientIdPolicy", policy =>
                policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
        });
    }
}