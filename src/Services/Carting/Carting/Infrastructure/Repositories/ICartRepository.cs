using Carting.Entities;

namespace Carting.Infrastructure.Repositories
{
    public interface ICartRepository
    {
        Task<CartItem?> AddCartItemAsync(int cartId, CartItem item);
        Task<Cart?> GetCartAsync(int cartId);
        Task<IEnumerable<CartItem>?> GetCartItemsAsync(int cartId);
        Task<bool> RemoveCartItemAsync(int cartId, CartItem item);
    }
}
