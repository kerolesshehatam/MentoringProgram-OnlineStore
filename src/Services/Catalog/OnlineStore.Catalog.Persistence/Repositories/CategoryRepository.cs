using Microsoft.EntityFrameworkCore;
using OnlineStore.Catalog.Application.Infrastructure;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _dbContext;

        public CategoryRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (category?.Id > 0)
            {
                return category;
            }
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category?.Id <= 0)
                throw new Exception("Invalid category");

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            var category = await GetCategoryAsync(id);
            if (category == null)
            {
                return false;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
