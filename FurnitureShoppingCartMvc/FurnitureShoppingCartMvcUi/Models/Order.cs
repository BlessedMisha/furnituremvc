using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FurnitureShoppingCartMvcUi.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderItemsJson { get; set; } /*зберігання списку товарів у вигляді JSON*/

        [NotMapped]
        public List<OrderItem> OrderItems // Це властивість для доступу до списку товарів
        {
            get => OrderItemsJson == null ? new List<OrderItem>() : JsonSerializer.Deserialize<List<OrderItem>>(OrderItemsJson);
            set => OrderItemsJson = JsonSerializer.Serialize(value);
        }
    }

    public class OrderItem
    {
        public int CatalogItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
