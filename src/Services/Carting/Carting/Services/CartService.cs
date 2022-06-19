using AutoMapper;
using Carting.Entities;
using Carting.Infrastructure.Repositories;
using OlineStore.CartingService.Models.Requests;
using OlineStore.CartingService.Models.Responses;

namespace OlineStore.CartingService.Services
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

        public async Task<CartItemResponse?> AddCartItemAsync(CartItemRequest request)
        {
            var item = _mapper.Map<CartItem>(request);

            var result = await _cartRepository.AddCartItemAsync(request.CartId, item);

            return result != null ? _mapper.Map<CartItemResponse>(result) : null;
        }

        public async Task<IEnumerable<CartItemResponse>?> GetCartItemsAsync(int cartId)
        {
            var result = await _cartRepository.GetCartItemsAsync(cartId);

            return result != null ? _mapper.Map<IEnumerable<CartItemResponse>>(result) : null;
        }

        public async Task<bool> RemoveCartItemAsync(CartItemRequest request)
        {
            var item = _mapper.Map<CartItem>(request);
            return await _cartRepository.RemoveCartItemAsync(request.CartId, item);
        }
    }
}
