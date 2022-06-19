namespace Carting.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }

        protected Cart()
        {

        }

        public Cart(int id)
        {
            Id = id;
            Items = new List<CartItem>();
        }
    }
}
