using FurnitureShoppingCartMvcUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FurnitureShoppingCartMvcUi.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace FurnitureShoppingCartMvcUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _context = context;
        }

        public IActionResult Index()
        {
            var catalogItems = _dbContext.CatalogItems
                .Select(e => e.Transform())
                .ToList();

            return View(catalogItems);
        }
        public IActionResult ProductDetails(int id)
        {
            var catalogItem = _dbContext.CatalogItems.Find(id);
            if (catalogItem == null)
            {
                return NotFound();
            }

            return View("ProductDetails", catalogItem.Transform()); // Передача моделі каталогового елемента на сторінку ProductDetails.cshtml
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
        public IActionResult Subscribe(SubscribeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Додати код для збереження підписника в базі даних тут

                    // Відправка підтверджувального електронного листа
                    SendConfirmationEmail(model.Email);

                    return Ok("Thank you for subscribing! Check your email for confirmation.");
                }
                catch (Exception ex)
                {
                    // Логування помилок або обробка помилок
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to subscribe. Please try again later.");
                }
            }

            return BadRequest(ModelState);
        }

        private void SendConfirmationEmail(string email)
        {
            var fromAddress = new MailAddress("m1fworkss@gmail.com", "M1f Works");
            var toAddress = new MailAddress(email);
            const string fromPassword = "ogol nkll yszn afzm";
            const string subject = "Welcome to Our Club!";
            const string body = "Dear Subscriber,\r\n\r\nThank you for joining our club! You are now part of our community and will be informed about all the latest updates and news on our website.\r\n\r\nStay tuned for exciting offers and exclusive content coming your way.\r\n\r\nBest regards,\r\n[M1f Works]";

            // Налаштування для відправлення електронної пошти через Gmail
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                EnableSsl = true,
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }
    }
}