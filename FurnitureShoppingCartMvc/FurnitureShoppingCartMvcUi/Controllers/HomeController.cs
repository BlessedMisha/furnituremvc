using FurnitureShoppingCartMvcUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FurnitureShoppingCartMvcUi.Data;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var catalogItems = _dbContext.CatalogItems
                .Select(e => e.Transform())
                .ToList();

            return View(catalogItems);
        }
            
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Shopall()
        {
            var catalogItems = _dbContext.CatalogItems
                .Select(e => e.Transform())
                .ToList();

            return View(catalogItems);
        }
        public IActionResult Basket()
        {
            var catalogItems = _dbContext.CatalogItems
                .Select(e => e.Transform())
                .ToList();

            return View(catalogItems);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
