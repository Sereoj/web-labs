using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductCatalogApp.Attributes;

namespace ProductCatalogApp.Models
{
    public class Order
    {
        [Key]
        public int CodeOrder { get; set; } // КодЗаказа (PK)

        [Required(ErrorMessage = "Выберите товар")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите товар")]
        public int CodeProduct { get; set; } // КодТовара (FK → Товар.КодТовара)

        [Required(ErrorMessage = "Количество обязательно для заполнения")]
        [PositiveQuantity]
        public int Quantity { get; set; } // Количество

        [Required(ErrorMessage = "Дата заказа обязательна")]
        public DateTime OrderDate { get; set; } // ДатаЗаказа

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; set; } // ОбщаяСтоимость

        // Navigation property
        [ForeignKey("CodeProduct")]
        public virtual Product Product { get; set; } = null!;
    }
}