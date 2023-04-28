using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Carts
    {
        [Key]
        public Guid id { get; set; }

        public ICollection<Cart_Items> items { get; set; }  
        public Customers customers { get; set; }

    }
}
