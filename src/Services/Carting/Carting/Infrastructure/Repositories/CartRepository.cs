using OnlineStore.CartingService.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace OnlineStore.CartingService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CartItem?> AddCartItemAsync(string cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId);

            if (cart == null)
                cart = new Cart(cartId);

            cart.Items.Add(item);
            var added = await UpdateBasketAsync(cart);
            return added ? item : null;
        }

        public async Task<bool> RemoveCartItemAsync(string cartId, int itemId)
        {
            var cart = await GetCartAsync(cartId);

            if (cart == null || !cart.Items.Any())
                return false;

            cart.Items = cart.Items.Where(t => t.Id != itemId).ToList();
            return await UpdateBasketAsync(cart);
        }

        public async Task<IEnumerable<CartItem>?> GetCartItemsAsync(string cartId)
        {
            var cart = await GetCartAsync(cartId);
            return cart?.Items;
        }
        public async Task<Cart?> GetCartAsync(string cartId)
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
