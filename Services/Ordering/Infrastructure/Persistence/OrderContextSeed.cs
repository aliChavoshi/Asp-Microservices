using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", nameof(OrderContext));
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "alich", FirstName = "chavoshi", LastName = "alichavoshi",
                EmailAddress = "alichavoshii1372@gmail.com",
                AddressLine = "Kashan", Country = "IRAN", TotalPrice = 350
            }
        };
    }
}