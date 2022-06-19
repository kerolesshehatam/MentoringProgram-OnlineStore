using AutoMapper;
using OnlineStore.Catalog.Application.Infrastructure;
using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Services
{
    public class CatalogService : ICatalogService
    {
        public readonly IMapper _mapper;
        private readonly ICategoryItemRepository _categoryItemRepository;

        public CatalogService(IMapper mapper, ICategoryItemRepository categoryItemRepository)
        {
            _mapper = mapper;
            _categoryItemRepository = categoryItemRepository;
        }

        public async Task<CategoryItemResponse?> AddCatalogItemAsync(CategoryItemRequest request)
        {
            var item = _mapper.Map<CategoryItem>(request);

            var result = await _categoryItemRepository.AddCatalogItemAsync(item);

            return result != null ? _mapper.Map<CategoryItemResponse>(result) : null;
        }

        public async Task<bool> DeleteCategoryItemAsync(int id)
        {
            return await _categoryItemRepository.DeleteCategoryItemAsync(id);
        }

        public async Task<CategoryItemResponse?> GetCategoryItemAsync(int id)
        {

            var result = await _categoryItemRepository.GetCategoryItemAsync(id);

            return result != null ? _mapper.Map<CategoryItemResponse>(result) : null;
        }

        public async Task<IEnumerable<CategoryItemResponse>?> GetCategoryItemsAsync(int categoryId)
        {
            var result = await _categoryItemRepository.GetCategoryItemsAsync(categoryId);

            return result != null ? _mapper.Map<IEnumerable<CategoryItemResponse>>(result) : null;
        }

        public async Task<CategoryItemResponse?> UpdateCatalogItemAsync(CategoryItemRequest request)
        {
            var item = _mapper.Map<CategoryItem>(request);

            var result = await _categoryItemRepository.UpdateCatalogItemAsync(item);

            return result != null ? _mapper.Map<CategoryItemResponse>(result) : null;
        }
    }
}
