using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Swagger.Core;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithVersioningAndAuth(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices;
        var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger()
            .UseAuthorization()
            .UseAuthentication();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());

            options.OAuthClientId("client_id_swagger");
            options.OAuthScopeSeparator(" ");
            options.OAuthClientSecret("client_secret_swagger");
            options.OAuthAppName("Client Credentials Client");
            //options.OAuthUsePkce();

        });

        return app;
    }

    public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices;
        var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());

        });

        return app;
    }
}