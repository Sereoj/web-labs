using ClosedXML.Excel;
using ProductCatalogApp.Data;
using ProductCatalogApp.Models;

namespace ProductCatalogApp.Services
{
    public interface IExcelImportService
    {
        Task ImportFromExcelAsync(string filePath);
    }

    public class ExcelImportService : IExcelImportService
    {
        private readonly ApplicationDbContext _context;

        public ExcelImportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ImportFromExcelAsync(string filePath)
        {
            using var workbook = new XLWorkbook(filePath);
            
            // Импорт категорий
            await ImportCategoriesAsync(workbook.Worksheet("Categories"));
            
            // Импорт товаров
            await ImportProductsAsync(workbook.Worksheet("Products"));
            
            // Импорт заказов
            await ImportOrdersAsync(workbook.Worksheet("Orders"));
            
            await _context.SaveChangesAsync();
        }

        private async Task ImportCategoriesAsync(IXLWorksheet worksheet)
        {
            if (worksheet == null) return;

            var rows = worksheet.RowsUsed().Skip(1); // Пропустить заголовок

            foreach (var row in rows)
            {
                var codeCategory = row.Cell(1).GetValue<int>();
                var nameCategory = row.Cell(2).GetValue<string>();

                // Проверяем, существует ли уже категория с таким кодом
                var existingCategory = await _context.Categories.FindAsync(codeCategory);
                
                if (existingCategory == null)
                {
                    var category = new Category
                    {
                        CodeCategory = codeCategory,
                        NameCategory = nameCategory ?? string.Empty
                    };
                    
                    _context.Categories.Add(category);
                }
                else
                {
                    // Обновляем существующую категорию
                    existingCategory.NameCategory = nameCategory ?? string.Empty;
                }
            }
        }

        private async Task ImportProductsAsync(IXLWorksheet worksheet)
        {
            if (worksheet == null) return;

            var rows = worksheet.RowsUsed().Skip(1); // Пропустить заголовок

            foreach (var row in rows)
            {
                var codeProduct = row.Cell(1).GetValue<int>();
                var nameProduct = row.Cell(2).GetValue<string>();
                var descriptionProduct = row.Cell(3).GetValue<string>();
                var price = row.Cell(4).GetValue<decimal>();
                var codeCategory = row.Cell(5).GetValue<int>();

                // Проверяем, существует ли уже товар с таким кодом
                var existingProduct = await _context.Products.FindAsync(codeProduct);
                
                if (existingProduct == null)
                {
                    var product = new Product
                    {
                        CodeProduct = codeProduct,
                        NameProduct = nameProduct ?? string.Empty,
                        DescriptionProduct = descriptionProduct ?? string.Empty,
                        Price = price,
                        CodeCategory = codeCategory
                    };
                    
                    _context.Products.Add(product);
                }
                else
                {
                    // Обновляем существующий товар
                    existingProduct.NameProduct = nameProduct ?? string.Empty;
                    existingProduct.DescriptionProduct = descriptionProduct ?? string.Empty;
                    existingProduct.Price = price;
                    existingProduct.CodeCategory = codeCategory;
                }
            }
        }

        private async Task ImportOrdersAsync(IXLWorksheet worksheet)
        {
            if (worksheet == null) return;

            var rows = worksheet.RowsUsed().Skip(1); // Пропустить заголовок

            foreach (var row in rows)
            {
                var codeOrder = row.Cell(1).GetValue<int>();
                var codeProduct = row.Cell(2).GetValue<int>();
                var quantity = row.Cell(3).GetValue<int>();
                var orderDate = row.Cell(4).GetValue<DateTime>();
                var totalCost = row.Cell(5).GetValue<decimal>();

                // Проверяем, существует ли уже заказ с таким кодом
                var existingOrder = await _context.Orders.FindAsync(codeOrder);
                
                if (existingOrder == null)
                {
                    var order = new Order
                    {
                        CodeOrder = codeOrder,
                        CodeProduct = codeProduct,
                        Quantity = quantity,
                        OrderDate = orderDate,
                        TotalCost = totalCost
                    };
                    
                    _context.Orders.Add(order);
                }
                else
                {
                    // Обновляем существующий заказ
                    existingOrder.CodeProduct = codeProduct;
                    existingOrder.Quantity = quantity;
                    existingOrder.OrderDate = orderDate;
                    existingOrder.TotalCost = totalCost;
                }
            }
        }
    }
}