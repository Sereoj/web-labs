using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть положительной")]
        public decimal Price { get; set; }
    }
}