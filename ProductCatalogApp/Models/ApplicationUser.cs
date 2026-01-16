using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApp.Models
{
    /// <summary>
    /// Расширенная модель пользователя ASP.NET Core Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Имя не может превышать 100 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Фамилия не может превышать 100 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [StringLength(500, ErrorMessage = "О себе не может превышать 500 символов")]
        [Display(Name = "О себе")]
        public string? About { get; set; }

        /// <summary>
        /// Предпочтительная категория товаров (выпадающий список из БД)
        /// </summary>
        [Display(Name = "Предпочтительная категория")]
        public int? PreferredCategoryId { get; set; }

        // Навигационное свойство для связи с категорией
        public virtual Category? PreferredCategory { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}

