using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger,
        CancellationToken cancellationToken)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders() =>
        new List<Order>
        {
            new()
            {
                UserName = "swn",
                FirstName = "Mehmet",
                LastName = "Ozkaya",
                EmailAddress = "ezozkme@gmail.com",
                AddressLine = "Bahcelievler",
                Country = "Turkey",
                TotalPrice = 350
            }
        };
}