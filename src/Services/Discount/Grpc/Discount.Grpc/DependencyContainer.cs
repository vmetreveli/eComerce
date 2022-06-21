using Discount.Grpc.Application;
using Discount.Grpc.Data.Repositories;
using Discount.Grpc.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc(options => { options.EnableDetailedErrors = true; });
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddControllers();

        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }
}