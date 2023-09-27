using BeuStoreApi.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class OrderItems
    {
        public Guid Id { get; set; }
        public Variants Variants { get; set; } = new Variants();
        public virtual Products Products { get; set; } = new Products();
        public virtual Orders Orders { get; set; } = new Orders();
        [Column(TypeName = "NUMERIC(18,2)")]
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
