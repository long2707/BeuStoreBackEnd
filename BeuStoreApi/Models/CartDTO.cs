using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Models
{
    public class CartDTO
    {
        
       // public Guid Customer { get; set; }
        public ICollection<CartItemDTO> CartItem { get; set; } = new List<CartItemDTO>();
        
    }
    public class CartItemDTO
    {
        [Required]
        public Guid productId { get; set; }
        public string variant { get; set; } = string.Empty;
        public int quantity { get; set; } = 1;

    }
}
