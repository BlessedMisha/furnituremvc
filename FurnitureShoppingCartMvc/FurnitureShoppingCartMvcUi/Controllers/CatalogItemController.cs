using FurnitureShoppingCartMvcUi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class CatalogItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ProductDetails(int id)
        {
            var catalogItem = _context.CatalogItems.Find(id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View(catalogItem.Transform()); // Передача моделі каталогового елемента на сторінку
        }
        public IActionResult Index()
        {
            var catalogItems = _context.CatalogItems.ToList();
            var catalogItemModels = catalogItems.Select(item => item.Transform()).ToList();
            return View(catalogItemModels);
        }
        public IActionResult Shopall()
        {
            var catalogItems = _context.CatalogItems.ToList();
            var catalogItemModels = catalogItems.Select(item => item.Transform()).ToList();
            return View(catalogItemModels);
        }
        public IActionResult Basket()
        {
            var catalogItems = _context.CatalogItems.ToList();
            var catalogItemModels = catalogItems.Select(item => item.Transform()).ToList();
            return View(catalogItemModels);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var catalogItem = _context.CatalogItems.Find(id);
            if (catalogItem == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "shopcart");
        }
        public IActionResult Search(string searchString)
        {
            // Отримати всі товари, які містять введений рядок в назві
            var filteredProducts = _context.CatalogItems
                .Where(p => p.Name.Contains(searchString))
                .Select(p => new CatalogItemModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    FullImageUrl=p.ImageUrl,
                    // Додайте інші властивості CatalogItemModel, які вам потрібні
                })
                .ToList();

            ViewData["FilterType"] = "Search"; // Додати тип фільтрації до ViewData

            // Отримати загальний список продуктів для відображення у боці
            var allProducts = _context.CatalogItems
                .Select(p => new CatalogItemModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    FullImageUrl = p.ImageUrl,
                    // Додайте інші властивості CatalogItemModel, які вам потрібні
                })
                .ToList();

            // Передати усі дані у представлення
            ViewData["ProductCount"] = allProducts.Count;
            return View("~/Views/Home/shopall.cshtml", filteredProducts);
        }


    }
}