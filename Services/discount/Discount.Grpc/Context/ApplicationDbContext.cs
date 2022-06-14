using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons => Set<Coupon>();
}