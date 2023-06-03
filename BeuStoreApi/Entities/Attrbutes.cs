using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Attrbutes
    {

        public Attrbutes() 
        {
            this.products = new HashSet<Products>();
            this.attrbuteValues = new HashSet<AttrbuteValue>();
        }

        public Guid id { get; set; }
        [Column(TypeName ="NVARCHAR(50)")]    
        public string atrribute_name { get; set; }    
        public DateTime create_at { get; set; }
        public virtual ICollection<Products> products { get; set; } 
        public virtual ICollection<AttrbuteValue> attrbuteValues { get; set; }
    }
}
