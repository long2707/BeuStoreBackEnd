using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class OrderItems
    {
        public Guid Id { get; set; }
        public Products Products { get; set; }
        public Orders Orders { get; set; }
        [Column(TypeName ="NUMERIC(18,2)")]
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
