using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class variant_options
    {
        [Key]
        public Guid id { get; set; }

        public string? SKU { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal sale_price { get; set; } 
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal compare_price { get; set; } 
        public int quantity { get; set; } 
        public Products products { get; set; } = new Products();
        public virtual ICollection<Variants> variants { get; set; } =new List<Variants>();

    }
}
