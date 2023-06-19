using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class ProductAttributeValues
    {
        [Key]
        public Guid id { get; set; }

        public AttrbuteValue AttrbuteValue { get; set; }= new AttrbuteValue();
        public ProductAttribute productAttribute { get; set; }= new ProductAttribute();
      //  public virtual ICollection<Variant_Values > Variant_Values { get; set; } = new HashSet<Variant_Values>();
    }
}
