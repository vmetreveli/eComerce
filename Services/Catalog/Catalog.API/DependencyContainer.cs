using System.IO.Compression;
using System.Text.Json;
using Catalog.API.Middleware;
using Catalog.Application;
using Catalog.Application.Behaviors;
using Catalog.Data;
using Catalog.Data.Context;
using Catalog.Data.Repositories;
using Catalog.Domain.Interfaces.Repository;
using FluentValidation;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
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
        //
        //
        // services.AddCore(configuration)
        //     .AddAuthentication(options =>
        //     {
        //         options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        //         options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        //         options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        //     })
        //     .AddJwtBearer("Bearer", options =>
        //     {
        //         options.SaveToken = true;
        //         options.Authority = configuration.GetValue<string>("IdentityProviderBaseUrl");
        //         options.RequireHttpsMetadata = false;
        //
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = false,
        //             ValidateIssuer = false,
        //             ValidateAudience = false
        //         };
        //
        //         options.Audience = "quality";
        //     });


        // services.AddAuthorization();
        //
        // services.AddResponseCaching();

        services.Configure<DatabaseSettings>(
            configuration.GetSection("DatabaseSettings"));

        services.AddScoped<ICatalogContext, CatalogContext>();

        /* Validation */
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        services.AddTransient<ExceptionHandlingMiddleware>();


     //   services.AddSerilogServices(configuration);

       // services.AddHealthChecks().AddCheck<DbConnectionHealthCheck>(nameof(DbConnectionHealthCheck));



        //services.AddScoped<IServiceCollection, ServiceCollection>();

        #region Repositories

        services.AddScoped<IProductRepository, ProductRepository>();
        #endregion
    }
}