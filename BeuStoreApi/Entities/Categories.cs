using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Categories
    {
        public Categories() 
        {
            this.Products = new HashSet<Products>();
        }


        [Key]
        public Guid categoryId { get; set; }
        public Guid? parent_id { get; set; } 
        [Required]
        public string category_Name { get; set; }
        = string.Empty;

        public string category_Description { get; set; }
        public virtual Categories Parent { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Categories> Children { get; set; }
    }
}
