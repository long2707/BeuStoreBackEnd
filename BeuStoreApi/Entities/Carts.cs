using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Carts
    {
        [Key]
        public Guid id { get; set; }

        public string token_cart { get; set; }= string.Empty;
        public virtual ICollection<Cart_Items> items { get; set; }  = new List<Cart_Items>();

    }
}
