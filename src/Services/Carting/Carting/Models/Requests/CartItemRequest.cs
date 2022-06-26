using System.ComponentModel.DataAnnotations;

namespace OnlineStore.CartingService.Models.Requests
{
    public class CartItemRequest
    {
        [Required]
        public string CartId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string? Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
