using Discount.API.Middleware;
using Discount.Domain.Interfaces.Repository;
using Discount.Infrastructure;
using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ordering.Application;

namespace Discount.API;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        // services.AddAutoMapper(typeof(Startup).Assembly);
        //
        // services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddControllers();

        // services.AddDbContext<UniDbContext>(options =>
        //     options.UseNpgsql(
        //         configuration.GetConnectionString("DefaultConnection")));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
                {Title = "Discount.API", Version = "v1"});
        });
        /* Validation */
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        // services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddSerilogServices(configuration);

        #region Repositories

        services.AddScoped<IDiscountRepository, DiscountRepository>();

        #endregion
    }
}