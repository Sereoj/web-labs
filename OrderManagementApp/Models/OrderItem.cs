using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть положительной")]
        public decimal Price { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть положительным")]
        public int Quantity { get; set; } = 1;
        
        public decimal GetTotalPrice()
        {
            return Price * Quantity;
        }
    }
}