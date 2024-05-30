using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using FurnitureShoppingCartMvcUi.Models;

namespace FurnitureShoppingCartMvcUi.Services
{
    public class LiqPayService
    {
        private readonly LiqPaySettings _settings;

        public LiqPayService(IOptions<LiqPaySettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetSignature(string data)
        {
            using (var sha1 = SHA1.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(_settings.PrivateKey + data + _settings.PrivateKey);
                var hash = sha1.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task<string> CreatePayment(decimal amount, string currency, string description)
        {
            var data = new
            {
                version = 3,
                public_key = _settings.PublicKey,
                action = "pay",
                amount = amount,
                currency = currency,
                description = description,
                order_id = Guid.NewGuid().ToString(),
                sandbox = 1
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonData));
            var signature = GetSignature(base64Data);

            var requestData = new Dictionary<string, string>
            {
                { "data", base64Data },
                { "signature", signature }
            };

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://www.liqpay.ua/api/3/checkout", new FormUrlEncodedContent(requestData));
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
