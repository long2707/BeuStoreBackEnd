using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Tags
    {
        public Tags()
        {
            this.Products = new HashSet<Products>();

        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string tag_name { get; set; } = string.Empty;
        public DateTime create_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set; } = DateTime.Now;
        public virtual ICollection<Products> Products { get; set; }
    }
}
