using FurnitureShoppingCartMvcUi.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class CatalogItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            // Додавання товару до корзини
            // Це може відрізнятися в залежності від вашої реалізації корзини

            return RedirectToAction("Index", "Cart"); // Перенаправте користувача на сторінку корзини
        }
    }
}