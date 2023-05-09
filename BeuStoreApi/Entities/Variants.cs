using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Variants
    {
        public Variants() 
        {
            this.VariantValues = new HashSet<VariantValues>();
        }
      
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid product_id { get; set; }

        public virtual Products products { get; set; }
        public virtual ICollection<AttrbuteValue> AttrbuteValue { get; set; }
        public virtual ICollection<VariantValues> VariantValues { get; set; }
    }
}
