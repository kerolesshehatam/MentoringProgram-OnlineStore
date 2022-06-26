namespace OnlineStore.CartingService.Models.Responses
{
    public class CartResponse
    {
        public string CartKey { get; set; }
        public List<CartItemResponse> Items { get; set; }
    }
}
