using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

[Table("CatalogItem")]
public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Size{ get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }

}

public class CatalogItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string FullImageUrl { get; set; }
}

public static class TransformExtensions
{
    public static CatalogItemModel Transform(this CatalogItem entity)
    {
        return new CatalogItemModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            FullImageUrl = entity.ImageUrl,
            Size = entity.Size,
            Description = entity.Description,
        };
    }
}