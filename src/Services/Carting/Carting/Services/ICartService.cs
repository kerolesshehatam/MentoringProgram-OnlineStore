using OlineStore.CartingService.Models.Requests;
using OlineStore.CartingService.Models.Responses;

namespace OlineStore.CartingService.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemResponse>?> GetCartItemsAsync(int cartId);
        Task<CartItemResponse?> AddCartItemAsync(CartItemRequest request);
        Task<bool> RemoveCartItemAsync(CartItemRequest request);
    }
}
