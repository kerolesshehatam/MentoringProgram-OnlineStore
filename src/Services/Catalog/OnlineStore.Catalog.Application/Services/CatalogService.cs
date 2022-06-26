using AutoMapper;
using OnlineStore.Catalog.Application.Infrastructure;
using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryItemRepository _categoryItemRepository;

        public CatalogService(IMapper mapper, ICategoryItemRepository categoryItemRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryItemRepository = categoryItemRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> GetCategoryAsync(int id)
        {

            var result = await _categoryRepository.GetCategoryAsync(id);

            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
        {

            var result = await _categoryRepository.GetCategoriesAsync();

            return _mapper.Map<IEnumerable<CategoryResponse>>(result);
        }

        public async Task<CategoryResponse> AddCategoryAsync(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            var result = await _categoryRepository.AddCategoryAsync(category);

            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            var result = await _categoryRepository.UpdateCategoryAsync(category);

            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }


        public async Task<CategoryItemResponse> GetCategoryItemAsync(int id)
        {

            var result = await _categoryItemRepository.GetCategoryItemAsync(id);

            return _mapper.Map<CategoryItemResponse>(result);
        }

        public async Task<IEnumerable<CategoryItemResponse>> GetCategoryItemsAsync(int categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _categoryItemRepository.GetCategoryItemsAsync(categoryId, pageIndex, pageSize);

            return _mapper.Map<IEnumerable<CategoryItemResponse>>(result);
        }

        public async Task<CategoryItemResponse> AddCategoryItemAsync(CategoryItemRequest request)
        {
            var item = _mapper.Map<CategoryItem>(request);

            var result = await _categoryItemRepository.AddCategoryItemAsync(item);

            return _mapper.Map<CategoryItemResponse>(result);
        }

        public async Task<CategoryItemResponse> UpdateCategoryItemAsync(CategoryItemRequest request)
        {
            var item = _mapper.Map<CategoryItem>(request);

            var result = await _categoryItemRepository.UpdateCategoryItemAsync(item);

            return _mapper.Map<CategoryItemResponse>(result);
        }


        public async Task<bool> DeleteCategoryItemAsync(int id)
        {
            return await _categoryItemRepository.DeleteCategoryItemAsync(id);
        }
    }
}
