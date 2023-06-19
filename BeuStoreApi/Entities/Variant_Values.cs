using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Variant_Values
    {
        [Key]
        public Guid id { get; set; }

      
        public Variants variants { get; set; } = new Variants();
        public  ProductAttributeValues product { get; set; } = new ProductAttributeValues();
    }
}
