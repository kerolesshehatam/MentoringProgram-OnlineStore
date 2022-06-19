using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;

namespace OnlineStore.Catalog.Application.Services
{
    public interface ICatalogService
    {
        Task<CategoryItemResponse?> AddCatalogItemAsync(CategoryItemRequest request);

        Task<bool> DeleteCategoryItemAsync(int id);

        Task<CategoryItemResponse?> GetCategoryItemAsync(int id);
        Task<IEnumerable<CategoryItemResponse>?> GetCategoryItemsAsync(int categoryId);
        Task<CategoryItemResponse?> UpdateCatalogItemAsync(CategoryItemRequest request);
    }
}
