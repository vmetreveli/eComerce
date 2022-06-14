using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swagger.Core.Options;

namespace Swagger.Core;

public static class ServiceExtensions
{
    private static IConfiguration? _configuration;
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        _configuration = configuration;

        return services.AddVersioning()
            .AddSwaggerVersioning();
    }

    private static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    private static IServiceCollection AddSwaggerVersioning(this IServiceCollection services)
    {


        services.ConfigureOptions<ConfigureSwaggerOptions>().AddSwaggerGen(options =>
        {
            // for further customization
            //options.OperationFilter<DefaultValuesFilter>();
            //  options.OperationFilter<SwaggerLanguageHeader>();
            // options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            // {
            //     Type = SecuritySchemeType.OAuth2,
            //     Flows = new OpenApiOAuthFlows
            //     {
            //
            //         Password = new OpenApiOAuthFlow
            //         {
            //             // AuthorizationUrl = new Uri("https://localhost:5443/connect/authorize"),
            //             // TokenUrl = new Uri("https://localhost:5443/connect/token"),
            //             //  AuthorizationUrl = new Uri($"{_configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/authorize"),
            //             TokenUrl = new Uri(
            //                 $"{_configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/token"),
            //             Scopes = new Dictionary<string, string>
            //             {
            //                 // {"HandbookAPI", "Demo API - full access"},
            //                 //
            //                 // {"HandbookAPI.read", "Demo API - read access"},
            //                 //
            //                 // {"HandbookAPI.write", "Demo API - write access"}
            //             }
            //         }
            //     }
            // });
            //
            // options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    } private static IServiceCollection AddSwaggerVersioningWithAuth(this IServiceCollection services)
    {


        services.ConfigureOptions<ConfigureSwaggerOptions>().AddSwaggerGen(options =>
        {
            // for further customization
            //options.OperationFilter<DefaultValuesFilter>();
            //  options.OperationFilter<SwaggerLanguageHeader>();
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {

                    Password = new OpenApiOAuthFlow
                    {
                        // AuthorizationUrl = new Uri("https://localhost:5443/connect/authorize"),
                        // TokenUrl = new Uri("https://localhost:5443/connect/token"),
                        //  AuthorizationUrl = new Uri($"{_configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/authorize"),
                        TokenUrl = new Uri(
                            $"{_configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            // {"HandbookAPI", "Demo API - full access"},
                            //
                            // {"HandbookAPI.read", "Demo API - read access"},
                            //
                            // {"HandbookAPI.write", "Demo API - write access"}
                        }
                    }
                }
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }
}