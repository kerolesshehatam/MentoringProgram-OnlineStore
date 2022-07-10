using OnlineStore.CartingService.Entities;

namespace OnlineStore.CartingService.Infrastructure.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<string> GetCartsIds();
        Task<Cart?> GetCartAsync(string cartId);
        Task<IEnumerable<CartItem>?> GetCartItemsAsync(string cartId);
        Task<CartItem?> AddCartItemAsync(string cartId, CartItem item);
        Task<bool> RemoveCartItemAsync(string cartId, int itemId);
        Task<bool> UpdateCartAsync(Cart cart);
    }
}
