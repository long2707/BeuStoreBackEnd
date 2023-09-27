using BeuStoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class ProductAttribute
    {
        public ProductAttribute()
        {
            this.ProductAttributeValues = new HashSet<ProductAttributeValues>();
        }
        [Key]
        public Guid id { get; set; }

        public virtual Products products { get; set; } = new Products();
        public virtual Attrbutes Attrbutes { get; set; } = new Attrbutes();
        public virtual ICollection<ProductAttributeValues> ProductAttributeValues { get; set; } 
    }
}
