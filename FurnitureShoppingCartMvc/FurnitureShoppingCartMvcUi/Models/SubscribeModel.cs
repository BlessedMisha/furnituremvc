using System.ComponentModel.DataAnnotations;

namespace FurnitureShoppingCartMvcUi.Models
{
    public class SubscribeModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    }