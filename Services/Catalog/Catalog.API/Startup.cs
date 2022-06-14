using Catalog.API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwaggerHandbook.Core;


namespace Catalog.API;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;
    private IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services) => RegisterServices(services, Configuration);

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.EnvironmentName == "Debug") app.UseDeveloperExceptionPage();

        //app.UseSwaggerWithVersioning();
        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseRequestLocalization();
        app.UseRouting();

        // app.UseAuthentication();
        // app.UseAuthorization();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            // endpoints.MapHealthChecks("/health", new HealthCheckOptions
            // {
            //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            // });
        });
    }

    private static void RegisterServices(IServiceCollection services, IConfiguration configuration) =>
        services.RegisterServices(configuration);
}