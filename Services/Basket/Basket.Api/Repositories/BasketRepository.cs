using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Repositories;
public class BasketRepository : IBasketRepository
{
	private readonly IDistributedCache distributedCache;

	public BasketRepository(IDistributedCache distributedCache)
	{
		this.distributedCache = distributedCache;
	}

	public async Task<ShoppingCart?> GetBasket(string userName)
	{
		// obtener el valor directamente de Redis
		var basket = await distributedCache.GetStringAsync(userName);

		if (basket is null)
			return null;

		return JsonSerializer.Deserialize<ShoppingCart>(basket);

	}

	public async Task<ShoppingCart?> UpdateBasket(ShoppingCart shoppingCart)
	{
		await distributedCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
		return await GetBasket(shoppingCart.UserName);
	}

	public async Task DeleteBasket(string userName)
		=> await distributedCache.RemoveAsync(userName);

}
