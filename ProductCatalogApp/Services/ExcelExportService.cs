using ClosedXML.Excel;
using ProductCatalogApp.Data;
using ProductCatalogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApp.Services
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportToExcelAsync();
    }

    public class ExcelExportService : IExcelExportService
    {
        private readonly ApplicationDbContext _context;

        public ExcelExportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> ExportToExcelAsync()
        {
            using var workbook = new XLWorkbook();
            
            // Экспорт категорий
            await AddCategoriesSheetAsync(workbook);
            
            // Экспорт товаров
            await AddProductsSheetAsync(workbook);
            
            // Экспорт заказов
            await AddOrdersSheetAsync(workbook);
            
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private async Task AddCategoriesSheetAsync(XLWorkbook workbook)
        {
            var categories = await _context.Categories.ToListAsync();
            
            var worksheet = workbook.Worksheets.Add("Categories");
            
            // Заголовки столбцов
            worksheet.Cell(1, 1).Value = "КодКатегории";
            worksheet.Cell(1, 2).Value = "НазваниеКатегории";
            
            // Форматирование заголовков
            var headerRange = worksheet.Range(1, 1, 1, 2);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            
            // Добавление данных
            int row = 2;
            foreach (var category in categories)
            {
                worksheet.Cell(row, 1).Value = category.CodeCategory;
                worksheet.Cell(row, 2).Value = category.NameCategory;
                row++;
            }
            
            // Автоматическая ширина столбцов
            worksheet.Columns().AdjustToContents();
        }

        private async Task AddProductsSheetAsync(XLWorkbook workbook)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            
            var worksheet = workbook.Worksheets.Add("Products");
            
            // Заголовки столбцов
            worksheet.Cell(1, 1).Value = "КодТовара";
            worksheet.Cell(1, 2).Value = "НазваниеТовара";
            worksheet.Cell(1, 3).Value = "ОписаниеТовара";
            worksheet.Cell(1, 4).Value = "Цена";
            worksheet.Cell(1, 5).Value = "КодКатегории";
            
            // Форматирование заголовков
            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            
            // Добавление данных
            int row = 2;
            foreach (var product in products)
            {
                worksheet.Cell(row, 1).Value = product.CodeProduct;
                worksheet.Cell(row, 2).Value = product.NameProduct;
                worksheet.Cell(row, 3).Value = product.DescriptionProduct;
                worksheet.Cell(row, 4).Value = product.Price;
                worksheet.Cell(row, 5).Value = product.CodeCategory;
                row++;
            }
            
            // Автоматическая ширина столбцов
            worksheet.Columns().AdjustToContents();
        }

        private async Task AddOrdersSheetAsync(XLWorkbook workbook)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();
            
            var worksheet = workbook.Worksheets.Add("Orders");
            
            // Заголовки столбцов
            worksheet.Cell(1, 1).Value = "КодЗаказа";
            worksheet.Cell(1, 2).Value = "КодТовара";
            worksheet.Cell(1, 3).Value = "Количество";
            worksheet.Cell(1, 4).Value = "ДатаЗаказа";
            worksheet.Cell(1, 5).Value = "ОбщаяСтоимость";
            
            // Форматирование заголовков
            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            
            // Добавление данных
            int row = 2;
            foreach (var order in orders)
            {
                worksheet.Cell(row, 1).Value = order.CodeOrder;
                worksheet.Cell(row, 2).Value = order.CodeProduct;
                worksheet.Cell(row, 3).Value = order.Quantity;
                worksheet.Cell(row, 4).Value = order.OrderDate;
                worksheet.Cell(row, 5).Value = order.TotalCost;
                row++;
            }
            
            // Автоматическая ширина столбцов
            worksheet.Columns().AdjustToContents();
        }
    }
}