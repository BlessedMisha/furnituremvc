// AdminController.cs

using FurnitureShoppingCartMvcUi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 5; // Number of orders per page

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Orders
        [HttpGet]
        public IActionResult Orders(int page = 1)
        {
            int totalOrders = _context.Orders.Count();
            int totalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);

            page = Math.Max(1, Math.Min(page, totalPages));

            List<Order> orders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewData["CurrentPage"] = page;

            return View("~/Views/Home/admin.cshtml", orders);
        }

        // GET: /Admin/GetOrder/{id}
        [HttpGet]
        public IActionResult GetOrder(int id)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: /Admin/EditOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /Admin/DeleteOrder/{id}
        [HttpDelete]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
