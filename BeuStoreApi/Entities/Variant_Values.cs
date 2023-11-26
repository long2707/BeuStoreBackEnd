
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Variant_Values
    {
        [Key]
        public Guid id { get; set; }


        public Variants variants { get; set; } = new Variants();
        public ProductAttributeValues productAttributeValue { get; set; } = new ProductAttributeValues();
    }
}
