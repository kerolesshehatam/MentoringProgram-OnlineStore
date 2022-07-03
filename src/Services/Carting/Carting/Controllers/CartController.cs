using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartingService.Models.Requests;
using OnlineStore.CartingService.Models.Responses;
using OnlineStore.CartingService.Services;
using System.Net;

namespace OnlineStore.CartingService.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/cart")]
    [ApiController]
    public partial class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet, Route("{id}")]
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

        [MapToApiVersion("2.0")]
        [HttpGet, Route("{id}")]
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

        [MapToApiVersion("1.0")]
        [HttpPost, Route("items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCartItemAsync([FromBody] CartItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            var result = await _cartService.AddCartItemAsync(request);

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete, Route("{cartId}/items/{itemId}")]
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