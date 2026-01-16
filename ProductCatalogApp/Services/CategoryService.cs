using ProductCatalogApp.Data;
using ProductCatalogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApp.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<IEnumerable<Category>> GetWithProductsAsync();
        Task<Category?> GetWithProductsByIdAsync(int id);
    }

    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetWithProductsAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetWithProductsByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CodeCategory == id);
        }
    }
}