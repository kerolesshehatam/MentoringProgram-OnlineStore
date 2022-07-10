using AutoMapper;
using OnlineStore.CartingService.Entities;
using OnlineStore.CartingService.Infrastructure.Repositories;
using OnlineStore.CartingService.Models.Requests;
using OnlineStore.CartingService.Models.Responses;

namespace OnlineStore.CartingService.Services
{
    public class CartService : ICartService
    {
        public readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public IEnumerable<string> GetCartsIds()
        {
            return _cartRepository.GetCartsIds();
        }
        public async Task<CartResponse> GetCartAsync(string cartId)
        {
            var result = await _cartRepository.GetCartAsync(cartId);

            return _mapper.Map<CartResponse>(result);
        }

        public async Task<IEnumerable<CartItemResponse>> GetCartItemsAsync(string cartId)
        {
            var result = await _cartRepository.GetCartItemsAsync(cartId);

            return _mapper.Map<IEnumerable<CartItemResponse>>(result);
        }
        public async Task UpdateCartItems(string cartId, int itemId, decimal newPrice, string newName)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);

            var itemsToUpdate = cart?.Items?.Where(x => x.Id == itemId).ToList();

            if (itemsToUpdate != null)
            {
                foreach (var item in itemsToUpdate)
                {
                    item.Name = newName;
                    item.Price = newPrice;
                }
                await _cartRepository.UpdateCartAsync(cart);
            }
        }
        public async Task<CartItemResponse> AddCartItemAsync(CartItemRequest request)
        {
            var item = _mapper.Map<CartItem>(request);

            var result = await _cartRepository.AddCartItemAsync(request.CartId, item);

            return _mapper.Map<CartItemResponse>(result);
        }


        public async Task<bool> RemoveCartItemAsync(string cartId, int itemId)
        {
            return await _cartRepository.RemoveCartItemAsync(cartId, itemId);
        }

    }
}
