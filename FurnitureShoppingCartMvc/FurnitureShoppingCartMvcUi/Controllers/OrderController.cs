using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FurnitureShoppingCartMvcUi.Data;
using FurnitureShoppingCartMvcUi.Models;
using Newtonsoft.Json;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public OrderController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            try
            {
                var order = new Order
                {
                    FirstName = orderRequest.FirstName,
                    LastName = orderRequest.LastName,
                    Email = orderRequest.Email,
                    Phone = orderRequest.Phone,
                    Address = orderRequest.Address,
                    TotalPrice = orderRequest.TotalPrice,
                    OrderItems = orderRequest.Items
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Генерація даних для LiqPay
                string publicKey = _configuration["LiqPay:PublicKey"];
                string privateKey = _configuration["LiqPay:PrivateKey"];
                var liqPayData = GenerateLiqPayData(order.Id, order.TotalPrice, "USD", publicKey, privateKey);

                return Ok(new
                {
                    orderId = order.Id,
                    data = liqPayData.data,
                    signature = liqPayData.signature
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private (string data, string signature) GenerateLiqPayData(int orderId, decimal amount, string currency, string publicKey, string privateKey)
        {
            var paymentData = new
            {
                version = 3,
                public_key = publicKey,
                action = "pay",
                amount = amount,
                currency = currency,
                description = "Order Payment",
                order_id = orderId.ToString(),
                result_url = "http://localhost:5000/Order/Success",  
                server_url = "http://localhost:5000/Order/Callback"  
            };

            string json = JsonConvert.SerializeObject(paymentData);
            string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            string signatureSource = privateKey + data + privateKey;
            string signature;

            using (var sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureSource));
                signature = Convert.ToBase64String(hash);
            }

            return (data, signature);
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            return Ok("Payment was successful!");
        }

        [HttpPost("Callback")]
        public IActionResult Callback()
        {
       
            return Ok();
        }
    }

    public class OrderRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
