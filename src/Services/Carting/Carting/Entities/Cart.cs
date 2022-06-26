namespace OnlineStore.CartingService.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }

        protected Cart()
        {

        }

        public Cart(string id)
        {
            Id = id;
            Items = new List<CartItem>();
        }
    }
}
