using ProductCatalogApp.Data;
using ProductCatalogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApp.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task<IEnumerable<Product>> GetWithCategoryAndOrdersAsync();
        Task<Product?> GetWithCategoryAndOrdersByIdAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetWithCategoryAsync();
    }

    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetWithCategoryAndOrdersAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Orders)
                .ToListAsync();
        }

        public async Task<Product?> GetWithCategoryAndOrdersByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Orders)
                .FirstOrDefaultAsync(p => p.CodeProduct == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CodeCategory == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}