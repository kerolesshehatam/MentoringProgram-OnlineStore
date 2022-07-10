using OnlineStore.CartingService.Models.Requests;
using OnlineStore.CartingService.Models.Responses;

namespace OnlineStore.CartingService.Services
{
    public interface ICartService
    {
        Task<CartResponse> GetCartAsync(string cartId);
        Task<IEnumerable<CartItemResponse>> GetCartItemsAsync(string cartId);
        Task<CartItemResponse> AddCartItemAsync(CartItemRequest request);
        Task<bool> RemoveCartItemAsync(string cartId, int itemId);
        Task UpdateCartItems(string cartId, int itemId, decimal newPrice, string newName);
        IEnumerable<string> GetCartsIds();
    }
}
