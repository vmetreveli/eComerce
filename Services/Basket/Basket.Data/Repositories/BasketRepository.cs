using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.Domain.Interfaces.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Data.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache) =>
        _redisCache = redisCache ?? throw new ArgumentException(nameof(redisCache));

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var basket = await _redisCache.GetStringAsync(userName, cancellationToken);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }


    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket, CancellationToken cancellationToken)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        return await GetBasket(basket.UserName, cancellationToken);
    }


    public async Task DeleteBasket(string userName, CancellationToken cancellationToken) =>
        await _redisCache.RemoveAsync(userName, cancellationToken);
}