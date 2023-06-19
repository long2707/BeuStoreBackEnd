using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
       
    public class AttrbuteValue
    {
        public AttrbuteValue() 
        { 
          //  this.AttributeValues = new HashSet<ProductAttributeValues>();
        }
        
        [Key]   
        public Guid Id { get; set; }
     
        public string attribute_value { get; set; } = string.Empty;

        
        public virtual Attrbutes Attrbutes { get; set; } = new Attrbutes();
     //   public virtual ICollection<ProductAttributeValues> AttributeValues { get; set; }
        
    }
}
