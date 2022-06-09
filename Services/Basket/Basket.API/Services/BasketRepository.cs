using System.Text.Json;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Services;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redis;

    public BasketRepository(IDistributedCache redis)
    {
        _redis = redis;
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var data = await _redis.GetStringAsync(userName);
        return data == null ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        var data = JsonSerializer.Serialize(basket);
        await _redis.SetStringAsync(basket.UserName, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });
        return basket;
    }

    public async Task DeleteBasket(string userName)
    {
        await _redis.RemoveAsync(userName);
    }
}