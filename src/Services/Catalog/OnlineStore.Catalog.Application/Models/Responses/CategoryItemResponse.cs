namespace OnlineStore.Catalog.Application.Models.Responses
{
    public class CategoryItemResponse
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse CategoryDetails { get; set; }
    }
}
