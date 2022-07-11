using Discount.API.Context;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var discountRepository = services.GetRequiredService<IDiscountRepository>();
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
        if (await context.Coupons.AnyAsync()) return host;
        var coupons = new List<Coupon>
        {
            new()
            {
                ProductName = "IPhone X",
                Amount = 150,
                Description = "IPhone Discount",
            },
            new()
            {
                ProductName = "Samsung 10",
                Description = "Samsung Discount",
                Amount = 100
            }
        };
        foreach (var coupon in coupons)
            await discountRepository.Create(coupon);
        return host;
    }
}