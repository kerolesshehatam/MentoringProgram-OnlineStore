using Microsoft.EntityFrameworkCore;
using OnlineStore.Catalog.Application.Infrastructure;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Persistence.Repositories
{
    public class CategoryItemRepository : ICategoryItemRepository
    {
        private readonly CatalogContext _dbContext;

        public CategoryItemRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<CategoryItem> GetCategoryItemAsync(int id)
        {
            return await _dbContext.CategoryItems.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<CategoryItem>> GetCategoryItemsAsync(int categoryId, int pageIndex = 1, int pageSize = 10)
        {
            return await _dbContext.CategoryItems.Where(t => t.CategoryId == categoryId)
                                                 .Skip(pageSize * (pageIndex - 1))
                                                 .Take(pageSize)
                                                 .Include(t => t.Category)
                                                 .ToListAsync();
        }
        public async Task<CategoryItem> AddCategoryItemAsync(CategoryItem item)
        {
            if (item?.Id > 0)
                return item;

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == item.CategoryId);
            if (category == null)
                throw new Exception("Invalid Category Item");

            await _dbContext.CategoryItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<CategoryItem> UpdateCategoryItemAsync(CategoryItem item)
        {
            if (item?.Id <= 0)
                throw new Exception("Invalid Category Item");

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == item.CategoryId);
            if (category == null)
                throw new Exception("Invalid Category Item");

            _dbContext.CategoryItems.Update(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
        public async Task<bool> DeleteCategoryItemAsync(int id)
        {
            if (id <= 0)
                return false;

            var item = await GetCategoryItemAsync(id);
            if (item == null)
                return false;

            _dbContext.CategoryItems.Remove(item);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
