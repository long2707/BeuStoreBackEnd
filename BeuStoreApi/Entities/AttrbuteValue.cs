using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
        [Table("Attribute_Value")]
    public class AttrbuteValue
    {
        public AttrbuteValue() { }

        [Key]
       
        public Guid Id { get; set; }
        public Guid attribute_id { get; set; }
        public string? attribute_value { get; set; }
        [Column(TypeName ="NVARCHAR(50)")]
        public string? color { get; set; }
        public Attrbutes Attrbutes { get; set; }
        public virtual ICollection<Variants> Variants { get; set; }  
    }
}
