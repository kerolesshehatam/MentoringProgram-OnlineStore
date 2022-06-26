using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;

namespace OnlineStore.Catalog.Application.Services
{
    public interface ICatalogService
    {
        Task<CategoryResponse> GetCategoryAsync(int id);
        Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();
        Task<CategoryResponse> AddCategoryAsync(CategoryRequest request);
        Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest request);
        Task<bool> DeleteCategoryAsync(int id);


        Task<CategoryItemResponse> GetCategoryItemAsync(int id);
        Task<IEnumerable<CategoryItemResponse>> GetCategoryItemsAsync(int categoryId, int pageIndex, int pageSize);
        Task<CategoryItemResponse> UpdateCategoryItemAsync(CategoryItemRequest request);
        Task<CategoryItemResponse> AddCategoryItemAsync(CategoryItemRequest request);
        Task<bool> DeleteCategoryItemAsync(int id);
    }
}
