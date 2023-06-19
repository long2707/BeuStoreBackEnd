using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
   
    //[Table("Products")]
    //[Index(nameof(product_name))]
    //[Index(nameof(slug), IsUnique =true)]
    public class Products
    {
        public Products() 
        {
            this.ProductCategories = new HashSet<Categories>();
            this.Gallerles = new HashSet<Gallerles>();
           // this.ProductAttributes = new HashSet<ProductAttribute>();
            this.Tags= new HashSet<Tags>();
         //   this.Cart_Items = new HashSet<Cart_Items>();
         //   this.Variant_Options= new HashSet<variant_options>();
        }
        [Key]
        public Guid Id { get; set; }
        public string product_name { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "TEXT")]
        public string slug { get; set; } = string.Empty;
        public string? SKU { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal sale_price { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal compare_price { get; set; }
        public int quantity { get; set; } 
        public string short_description { get; set; } = string.Empty;
        [Column(TypeName ="TEXT")]
        public string product_description { get; set; }= string.Empty;
        public bool published { get; set; }= false;
        public string product_type { get; set;} = string.Empty;
        public DateTime? created_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; } = DateTime.Now;

        public virtual ICollection<Categories> ProductCategories { get; }
        public virtual ICollection<Gallerles> Gallerles { get; set; }   
        //public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }   
        public virtual ICollection<Tags> Tags { get; set; }
        //public virtual ICollection<variant_options> Variant_Options { get; set; }
        //public virtual ICollection<Variants> Variants { get; set; } = new HashSet<Variants>();
        //public virtual ICollection<Cart_Items> Cart_Items { get; set;}
    }
}
