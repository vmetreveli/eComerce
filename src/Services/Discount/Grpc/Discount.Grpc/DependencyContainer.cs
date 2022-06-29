using Discount.Grpc.Application;
using Discount.Grpc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc;

public static class DependencyContainer
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc(options => { options.EnableDetailedErrors = true; });
        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        services.AddControllers();

    }
}