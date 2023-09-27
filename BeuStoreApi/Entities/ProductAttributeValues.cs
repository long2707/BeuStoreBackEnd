using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class ProductAttributeValues
    {
        [Key]
        public Guid id { get; set; }

      
        public virtual AttrbuteValue AttrbuteValue { get; set; } = new AttrbuteValue();

        public virtual ProductAttribute ProductAttribute { get; set; } = new ProductAttribute();

       // public virtual ICollection<Variant_Values > Variant_Values { get; set; } = new HashSet<Variant_Values>();
    }
}
