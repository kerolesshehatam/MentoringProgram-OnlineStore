using OnlineStore.CartingService.Entities;

namespace OnlineStore.CartingService.Infrastructure.Repositories
{
    public interface ICartRepository
    {
        Task<CartItem?> AddCartItemAsync(string cartId, CartItem item);
        Task<Cart?> GetCartAsync(string cartId);
        Task<IEnumerable<CartItem>?> GetCartItemsAsync(string cartId);
        Task<bool> RemoveCartItemAsync(string cartId, int itemId);
    }
}
