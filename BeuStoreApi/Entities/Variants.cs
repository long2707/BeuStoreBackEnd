using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Variants
    {
        [Key]
        public Guid Id { get; set; }

        public string variant_option = string.Empty;
    
        public Products products { get; set; } = new Products();
        public variant_options Variant_Options { get; set; }= new variant_options();

        public virtual ICollection<Cart_Items> Cart_Items { get; set; }=new HashSet<Cart_Items>();
        public virtual ICollection<OrderItems> Order_Items { get; set; } = new HashSet<OrderItems>();
        public virtual ICollection<Variant_Values> variants { get; set; } = new HashSet<Variant_Values>();
    }
}
