using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CartingService.Models.Responses;

namespace OnlineStore.CartingService.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/gcart")]
    [ApiController]
    public partial class GCartController : ControllerBase
    {
        public GCartController(ILogger<CartController> logger)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("item/details/{id}")]
        public ActionResult GetCartItemDetails(int id)
        {
            var itemDetails = new CartItemResponse()
            {
                Id = id,
                Name = "test item 1",
                Price = 10,
                Quantity = 10,
                Image = "image"
            };

            return Ok(itemDetails);
        }
    }
}