using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApp.Models;

namespace ProductCatalogApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Вызываем базовый метод для настройки Identity
            base.OnModelCreating(modelBuilder);

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CodeCategory);
                entity.Property(e => e.NameCategory).IsRequired().HasMaxLength(255);
                entity.ToTable("Categories");
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.CodeProduct);
                entity.Property(e => e.NameProduct).IsRequired().HasMaxLength(255);
                entity.Property(e => e.DescriptionProduct).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.CodeCategory)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.ToTable("Products");
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.CodeOrder);
                entity.Property(e => e.OrderDate).HasColumnType("datetime2");
                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");
                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.CodeProduct)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.ToTable("Orders");
            });

            // Configure ApplicationUser entity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.About).HasMaxLength(500);
                
                entity.HasOne(u => u.PreferredCategory)
                      .WithMany()
                      .HasForeignKey(u => u.PreferredCategoryId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
