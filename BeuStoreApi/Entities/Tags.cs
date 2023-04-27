using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Tags
    {
        public Tags()
        {
            this.products = new HashSet<Products>();
        }
        [Key]
       public Guid id { get; set; }
        [Required]
        public string tag_name { get; set; }
        public virtual ICollection<Products> products { get; set; } 

    }
}
