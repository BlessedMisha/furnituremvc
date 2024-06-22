using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FurnitureShoppingCartMvcUi.Models;
using FurnitureShoppingCartMvcUi.Data;
using Microsoft.AspNetCore.Authorization;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /admin/orders
        public IActionResult Orders()
        {
            List<Order> orders = _context.Orders.ToList();
            return View("~/Views/Home/admin.cshtml", orders);
        }
    }
}
