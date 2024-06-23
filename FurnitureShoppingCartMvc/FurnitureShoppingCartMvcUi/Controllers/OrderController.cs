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
                    OrderItems = orderRequest.Items,
                    IsPaid = false,
                    OrderDate = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

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
                result_url = $"http://localhost:5241/Order/Success?orderId={orderId}",
                server_url = $"http://localhost:5241/api/Order/Callback"
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

        private bool VerifyLiqPaySignature(string data, string signature)
        {
            string privateKey = _configuration["LiqPay:PrivateKey"];
            string expectedSignature;

            using (var sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(privateKey + data + privateKey));
                expectedSignature = Convert.ToBase64String(hash);
            }

            return signature == expectedSignature;
        }

        [HttpPost("Callback")]
        public async Task<IActionResult> Callback([FromForm] LiqPayCallbackModel model)
        {
            try
            {
                string data = Request.Form["data"];
                string signature = Request.Form["signature"];

                if (!VerifyLiqPaySignature(data, signature))
                {
                    Console.WriteLine("Invalid LiqPay signature");
                    return BadRequest("Invalid LiqPay signature");
                }

                string decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(data));
                dynamic paymentData = JsonConvert.DeserializeObject(decodedData);

                int orderId = int.Parse(paymentData.order_id.ToString());
                string status = paymentData.status;

                Console.WriteLine($"Received LiqPay callback for order ID: {orderId}, status: {status}");

                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return NotFound();
                }

                if (status == "success")
                {
                    order.IsPaid = true;
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in processing LiqPay callback: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Success")]
        public async Task<IActionResult> Success(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return NotFound();
                }

                if (!order.IsPaid)
                {
                    order.IsPaid = true;
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
}