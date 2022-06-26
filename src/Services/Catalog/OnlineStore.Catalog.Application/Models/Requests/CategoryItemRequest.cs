using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Catalog.Application.Models.Requests
{
    public class CategoryItemRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
