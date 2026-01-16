using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApp.Models
{
    public class Category
    {
        [Key]
        public int CodeCategory { get; set; } // КодКатегории (PK)

        [Required(ErrorMessage = "Название категории обязательно для заполнения")]
        [StringLength(255, ErrorMessage = "Название категории не может превышать 255 символов")]
        public string NameCategory { get; set; } = string.Empty; // НазваниеКатегории

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}