using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShoppingCartMvcUi.Models
{
    [Table("Furniture")]
    public class Furniture
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]

        public string? FurnitureName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
            
        public int ColectionId { get; set; }
        public Colection Colection { get; set; }
        public List<OrderDetail> OrderDetail  { get; set; }
        public List<CartDetail> CartDetail { get; set; }

    }
}
