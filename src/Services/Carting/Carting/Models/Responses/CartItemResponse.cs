namespace OnlineStore.CartingService.Models.Responses
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
