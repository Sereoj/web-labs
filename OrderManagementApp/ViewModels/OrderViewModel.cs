using OrderManagementApp.Models;

namespace OrderManagementApp.ViewModels
{
    public class OrderViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public Order Order { get; set; } = new Order();
    }
}