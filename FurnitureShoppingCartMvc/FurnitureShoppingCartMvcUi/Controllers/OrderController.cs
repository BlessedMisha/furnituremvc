using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using FurnitureShoppingCartMvcUi.Data;
namespace FurnitureShoppingCartMvcUi.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return Ok(new { orderId = order.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
