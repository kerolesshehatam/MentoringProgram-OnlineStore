namespace OnlineStore.Catalog.Application.Models.Requests
{
    public class CategoryItemRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
