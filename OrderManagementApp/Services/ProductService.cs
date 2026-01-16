using OrderManagementApp.Models;

namespace OrderManagementApp.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ноутбук", Price = 50000 },
            new Product { Id = 2, Name = "Мышь", Price = 1500 },
            new Product { Id = 3, Name = "Клавиатура", Price = 3000 },
            new Product { Id = 4, Name = "Монитор", Price = 25000 },
            new Product { Id = 5, Name = "Наушники", Price = 4000 }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}