using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public int CatalogItemId { get; set; }
    public string ItemName { get; set; }
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
}
}
