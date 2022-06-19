namespace OlineStore.CartingService.Models.Requests
{
    public class CartItemRequest
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
