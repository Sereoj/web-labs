using OrderManagementApp.Models;

namespace OrderManagementApp.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
    }
}