namespace OnlineStore.Catalog.Application.Models.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }

        public CategoryResponse? Partent { get; set; }

    }
}
