using Discount.Grpc.Context;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Discount.Grpc.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var discountRepository = services.GetRequiredService<IDiscountRepository>();
        var context = services.GetRequiredService<ApplicationDbContext>();
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