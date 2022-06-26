using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Catalog.Application.Models.Requests
{
    public class CategoryRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        [Url]
        public string? Image { get; set; }
        public int? PartentId { get; set; }
    }
}
