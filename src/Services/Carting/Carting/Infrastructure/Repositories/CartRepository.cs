using Carting.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Carting.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CartItem?> AddCartItemAsync(int cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId);

            if (cart == null)
                cart = new Cart(cartId);

            cart.Items.Add(item);
            var added = await UpdateBasketAsync(cart);
            return added ? item : null;
        }

        public async Task<bool> RemoveCartItemAsync(int cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId);

            if (cart == null || !cart.Items.Any())
                return false;

            cart.Items = cart.Items.Where(t => t.Id != item.Id).ToList();
            return await UpdateBasketAsync(cart);
        }

        public async Task<IEnumerable<CartItem>?> GetCartItemsAsync(int cartId)
        {
            var cart = await GetCartAsync(cartId);
            return cart?.Items;
        }
        public async Task<Cart?> GetCartAsync(int cartId)
        {
            var cart = await _database.StringGetAsync(cartId.ToString());

            if (cart.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<Cart>(cart);
        }

        private async Task<bool> UpdateBasketAsync(Cart cart)
        {
            return await _database.StringSetAsync(cart.Id.ToString(), JsonSerializer.Serialize(cart));
        }
    }
}
