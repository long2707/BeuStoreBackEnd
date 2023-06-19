using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    
    public class ProductAttribute
    {
        public ProductAttribute() 
        { 
         // this.ProductAttributeValues = new HashSet<ProductAttributeValues>();
        }
        [Key]
        [Required]
        public Guid id { get; set; }

        public virtual Products products { get; set; } = new Products();
        public virtual Attrbutes Attrbutes { get; set; } = new Attrbutes();
     //   public virtual ICollection<ProductAttributeValues> ProductAttributeValues { get; set; } 

    }
}
