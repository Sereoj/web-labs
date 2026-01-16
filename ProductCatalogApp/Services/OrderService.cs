using ProductCatalogApp.Data;
using ProductCatalogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApp.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetWithProductAsync();
        Task<Order?> GetWithProductByIdAsync(int id);
        Task<IEnumerable<Order>> GetByProductAsync(int productId);
        Task<Order> CreateWithCalculationAsync(Order order);
    }

    public class OrderService : GenericService<Order>, IOrderService
    {
        public OrderService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetWithProductAsync()
        {
            return await _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task<Order?> GetWithProductByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.CodeOrder == id);
        }

        public async Task<IEnumerable<Order>> GetByProductAsync(int productId)
        {
            return await _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .Where(o => o.CodeProduct == productId)
                .ToListAsync();
        }

        public async Task<Order> CreateWithCalculationAsync(Order order)
        {
            // Calculate TotalCost based on product price and quantity
            var product = await _context.Products.FindAsync(order.CodeProduct);
            if (product != null)
            {
                order.TotalCost = product.Price * order.Quantity;
            }
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}