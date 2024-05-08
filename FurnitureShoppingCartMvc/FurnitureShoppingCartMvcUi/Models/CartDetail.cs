using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShoppingCartMvcUi.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public  int ShoppingCartId { get; set; }
        [Required]
        public int FurnitureId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Furniture Furniture { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}