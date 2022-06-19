using Microsoft.AspNetCore.Mvc;
using OlineStore.CartingService.Models.Requests;
using OlineStore.CartingService.Models.Responses;
using OlineStore.CartingService.Services;

namespace OlineStore.CartingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet(Name = "GetCart")]
        public async Task<ActionResult<CartItemResponse>> Get()
        {
            var request = new CartItemRequest() { CartId = 1, ItemId = 1, Name = "Item 1", Price = 10, Quantity = 12 };
            var result = await _cartService.AddCartItemAsync(request);

            return Ok(result);
        }
    }
}