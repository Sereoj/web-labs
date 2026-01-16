using Microsoft.AspNetCore.Identity;
using ProductCatalogApp.Models;

namespace ProductCatalogApp.Services
{
    /// <summary>
    /// Сервис для инициализации ролей и начального пользователя-администратора
    /// </summary>
    public static class IdentityDataInitializer
    {
        /// <summary>
        /// Названия ролей в системе
        /// </summary>
        public static class Roles
        {
            public const string Administrator = "Администратор";
            public const string User = "Пользователь";
        }

        /// <summary>
        /// Инициализирует роли и администратора при первом запуске
        /// </summary>
        public static async Task InitializeAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Создание ролей
            await CreateRoleAsync(roleManager, Roles.Administrator);
            await CreateRoleAsync(roleManager, Roles.User);

            // Создание администратора по умолчанию
            await CreateAdminUserAsync(userManager);
        }

        private static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);
                
                if (!result.Succeeded)
                {
                    throw new Exception($"Ошибка создания роли '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private static async Task CreateAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "admin@example.com";
            const string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Администратор",
                    LastName = "Системы",
                    RegistrationDate = DateTime.UtcNow,
                    About = "Главный администратор системы"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                
                if (!result.Succeeded)
                {
                    throw new Exception($"Ошибка создания администратора: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                // Назначаем роль администратора
                await userManager.AddToRoleAsync(adminUser, Roles.Administrator);
            }
        }
    }
}

