using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BeuStoreApi.Entities;

namespace BeuStoreApi.Entities
{
    public class Cart_Items
    {
        [Key]
        public Guid Id { get; set; }

        public Variants Variants { get; set; } = new Variants();
        public virtual Products Products { get; set; } = new Products();
        public virtual Carts Carts { get; set; } = new Carts();
        public int quantity { get; set; }
        [Column(TypeName = "NUMERIC(18,2)")]
        public decimal price { get; set; }
    }
}
