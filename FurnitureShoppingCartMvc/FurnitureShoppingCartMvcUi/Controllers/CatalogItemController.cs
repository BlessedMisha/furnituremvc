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
    }
}