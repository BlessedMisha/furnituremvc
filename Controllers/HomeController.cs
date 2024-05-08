using FurnitureShoppingCartMvcUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<CatalogItem> catalogItems = new List<CatalogItem>()
            {
                new CatalogItem() { Id = 1, Name = "Item1", Price=3},
                new CatalogItem() { Id = 1, Name = "Item2", Price=5},
            };

            return View(catalogItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
