using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
   // [Index(nameof(category_slug), IsUnique =true)]
   
    public class Categories
    {
        public Categories() 
        {
            this.ProductCategories = new HashSet<Products>();
            this.Children = new HashSet<Categories>();
        }

        [Key]
        public Guid categoryId { get; set; }
     
        public Guid? parent_id { get; set; } 
        [Required]
        public string category_Name { get; set; }
        = string.Empty;
        public string category_slug { get; set; } = string.Empty;
       // public IFormFile? image { get; set; }
        [Column(TypeName = "TEXT")]
        public string category_Description { get; set; }= string.Empty;
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? update_at { get; set; } = DateTime.Now;
        
        public virtual Categories? Parent { get; set; }
        public virtual ICollection<Products> ProductCategories { get; set; }
        public virtual ICollection<Categories> Children { get; set; } = new HashSet<Categories>();  
    }
}
