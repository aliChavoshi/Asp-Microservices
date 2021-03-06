using Discount.Grpc.Context;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _context;

    public DiscountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Coupon> Get(string productName)
    {
        return await _context?.Coupons?.FirstOrDefaultAsync(c => c.ProductName == productName)!;
    }

    public async Task<List<Coupon>> Get()
    {
        return await _context.Coupons.ToListAsync();
    }

    public async Task<bool> Create(Coupon coupon)
    {
        await _context.Coupons.AddAsync(coupon);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Coupon coupon)
    {
        _context.Coupons.Update(coupon);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Coupon coupon)
    {
        _context.Coupons.Remove(coupon);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(string productName)
    {
        var coupon = await Get(productName);
        return await Delete(coupon).ConfigureAwait(false);
    }
}