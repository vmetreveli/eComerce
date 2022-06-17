using System.Reflection.Metadata;
using Discount.Grpc.Data.Repositories;
using Discount.Grpc.Domain.Interfaces.Repository;
using Discount.Grpc.Middleware;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(Discount.Grpc.Application.AssemblyReference).Assembly);

        services.AddControllers();

        // services.AddDbContext<UniDbContext>(options =>
        //     options.UseNpgsql(
        //         configuration.GetConnectionString("DefaultConnection")));


        // /* Validation */
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        // services.AddValidatorsFromAssembly(typeof(Discount.Application.AssemblyReference).Assembly);
        //
        services.AddTransient<ExceptionHandlingMiddleware>();
        //
        // services.AddSerilogServices(configuration);

        #region Repositories

        services.AddScoped<IDiscountRepository, DiscountRepository>();

        #endregion
    }
}