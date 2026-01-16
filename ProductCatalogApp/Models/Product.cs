using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductCatalogApp.Attributes;

namespace ProductCatalogApp.Models
{
    public class Product
    {
        [Key]
        public int CodeProduct { get; set; } // КодТовара (PK)

        [Required(ErrorMessage = "Название товара обязательно для заполнения")]
        [StringLength(255, ErrorMessage = "Название товара не может превышать 255 символов")]
        public string NameProduct { get; set; } = string.Empty; // НазваниеТовара

        [StringLength(1000, ErrorMessage = "Описание товара не может превышать 1000 символов")]
        public string DescriptionProduct { get; set; } = string.Empty; // ОписаниеТовара

        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [PositivePrice]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } // Цена

        [Required(ErrorMessage = "Выберите категорию")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите категорию")]
        public int CodeCategory { get; set; } // КодКатегории (FK → КатегорияТовара.КодКатегории)

        // Navigation properties
        [ForeignKey("CodeCategory")]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}