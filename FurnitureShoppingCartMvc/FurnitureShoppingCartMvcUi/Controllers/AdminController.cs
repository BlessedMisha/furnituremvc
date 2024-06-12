using Microsoft.AspNetCore.Mvc;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
