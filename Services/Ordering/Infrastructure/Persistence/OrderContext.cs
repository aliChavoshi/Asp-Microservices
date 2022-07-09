using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "ali";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "ali";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}