using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShoppingCartMvcUi.Models
{
    [Table("Colection")]
    public class Colection
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]

        public string ColectionName { get; set; }
        public List<Furniture> Furnitures { get; set; }
  
    }
}
