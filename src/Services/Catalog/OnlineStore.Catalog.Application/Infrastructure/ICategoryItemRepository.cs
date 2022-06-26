using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Infrastructure
{
    public interface ICategoryItemRepository
    {
        Task<CategoryItem> GetCategoryItemAsync(int id);
        Task<IEnumerable<CategoryItem>> GetCategoryItemsAsync(int categoryId, int pageIndex, int pageSize);
        Task<CategoryItem> AddCategoryItemAsync(CategoryItem item);
        Task<CategoryItem> UpdateCategoryItemAsync(CategoryItem item);
        Task<bool> DeleteCategoryItemAsync(int id);
    }
}
