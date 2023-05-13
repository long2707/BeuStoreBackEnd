using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
   
    [Table("Products")]
    [Index(nameof(product_name))]
    public class Products
    {
        public Products() 
        {
            this.Categories = new HashSet<Categories>();
            this.Gallerles = new HashSet<Gallerles>();
            this.Attrbutes = new HashSet<Attrbutes>();
            this.Tags= new HashSet<Tags>();
            this.Cart_Items = new HashSet<Cart_Items>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
       
        public string product_name { get; set; }
        public string SKU { get; set; }

        [Column(TypeName ="numeric(18,2)")]
        [Required]
        public decimal regular_price { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal? discount_price { get; set; }
        [Required]
        public int quantity { get; set; }
        [Column(TypeName ="TEXT")]
        public string product_description { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; } = DateTime.Now;

        public Guid createed_by { get; set; }
        public Guid updated_by { get; set;}
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Gallerles> Gallerles { get; set; }   
        public virtual ICollection<Attrbutes> Attrbutes { get; set; }   
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<Cart_Items> Cart_Items { get; set;}
    }
}
