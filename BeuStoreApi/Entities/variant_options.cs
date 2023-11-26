
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BeuStoreApi.Entities;

namespace BeuStoreApi.Entities
{
    public class variant_options
    {
        public variant_options()
        {
            this.variants = new HashSet<Variants>();
        }
        [Key]
        public Guid id { get; set; }

        public string title { get; set; }
        public string? SKU { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal sale_price { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal compare_price { get; set; }
        public int quantity { get; set; }
        public Products products { get; set; } = new Products();
        public virtual ICollection<Variants> variants { get; set; } = new List<Variants>();

    }
}
