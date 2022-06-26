using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
