using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartingService.Models.Requests;
using OnlineStore.CartingService.Models.Responses;
using OnlineStore.CartingService.Services;
using System.Net;

namespace OnlineStore.CartingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        [HttpGet]
        [Route("v1/cart/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartResponse>> GetCartById(string id)
        {
            var cart = await _cartService.GetCartAsync(id);
            if (cart == null)
            {
                _logger.LogError($"Cart with id: {id}, not found.");

                return NotFound();
            }

            return Ok(cart);
        }

        [HttpGet]
        [Route("v2/cart/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<CartItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartItemResponse>> GetCartByIdV2(string id)
        {
            var cart = await _cartService.GetCartItemsAsync(id);
            if (cart == null)
            {
                _logger.LogError($"Cart with id: {id}, not found.");
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost]
        [Route("v1/cart/{id}/items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCartItemAsync(string cartId, [FromBody] CartItemRequest request)
        {
            if (!ModelState.IsValid || cartId is null || cartId != request.CartId)
                return BadRequest("Invalid request");

            var result = await _cartService.AddCartItemAsync(request);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/cart/{cartId}/Items/{itemId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCartItemAsync(string cartId, int itemId)
        {
            if (!ModelState.IsValid || cartId is null || itemId == 0)
                return BadRequest("Invalid request");

            var deleted = await _cartService.RemoveCartItemAsync(cartId, itemId);
            if (!deleted)
                return NotFound("Cart not found");

            return Ok(true);
        }
    }
}