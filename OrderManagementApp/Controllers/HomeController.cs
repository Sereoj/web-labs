using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;
using OrderManagementApp.Services;
using OrderManagementApp.ViewModels;

namespace OrderManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var order = HttpContext.Session.GetObject<Order>("Order") ?? new Order();
            var viewModel = new OrderViewModel
            {
                Products = _productService.GetAllProducts().ToList(),
                Order = order
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToOrder(int productId)
        {
            // Проверяем, что productId существует в списке продуктов
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return BadRequest("Товар не найден");
            }

            var order = HttpContext.Session.GetObject<Order>("Order") ?? new Order();

            var existingItem = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObject("Order", order);

            return Ok(new { success = true, totalAmount = order.GetTotalAmount() });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            // Проверяем, что productId существует в списке продуктов
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return BadRequest("Товар не найден");
            }

            // Проверяем, что количество в допустимом диапазоне
            if (quantity < 0 || quantity > 1000) // Максимальное количество 1000
            {
                return BadRequest("Недопустимое количество");
            }

            var order = HttpContext.Session.GetObject<Order>("Order") ?? new Order();

            var item = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    order.Items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }

                HttpContext.Session.SetObject("Order", order);
            }

            return Json(new { success = true, totalAmount = order.GetTotalAmount() });
        }

        [HttpPost]
        public IActionResult RemoveFromOrder(int productId)
        {
            // Проверяем, что productId существует в списке продуктов
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return BadRequest("Товар не найден");
            }

            var order = HttpContext.Session.GetObject<Order>("Order") ?? new Order();

            var item = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                order.Items.Remove(item);
                HttpContext.Session.SetObject("Order", order);
            }

            return Json(new { success = true, totalAmount = order.GetTotalAmount() });
        }

        [HttpPost]
        public IActionResult ClearOrder()
        {
            HttpContext.Session.Remove("Order");
            return Json(new { success = true, totalAmount = 0 });
        }
    }
}