using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Infrastructure
{
    public interface ICategoryItemRepository
    {
        Task<CategoryItem?> AddCatalogItemAsync(CategoryItem item);

        Task<bool> DeleteCategoryItemAsync(int id);

        Task<CategoryItem?> GetCategoryItemAsync(int id);
        Task<IEnumerable<CategoryItem>?> GetCategoryItemsAsync(int categoryId);
        Task<CategoryItem?> UpdateCatalogItemAsync(CategoryItem item);
    }
}
