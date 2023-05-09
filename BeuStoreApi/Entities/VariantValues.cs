using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class VariantValues
    {
        public VariantValues() { }

        [Key]
        public Guid id { get;set; }

        public Guid variant_id { get;set ; }
        [Column(TypeName ="NUMERIC(18,2)")]
        public decimal price { get;set; }
        public int quantity { get;set; }
        public virtual Variants variants { get; set; }
    }
}
