using Discount.Grpc.Application.Contracts.Infrastructure;
using Discount.Grpc.Infrastructure.Persistence;
using Discount.Grpc.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UniDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDiscountRepository, DiscountRepository>();

        return services;
    }
}