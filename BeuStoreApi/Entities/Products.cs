using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BeuStoreApi.Entities;

namespace BeuStoreApi.Entities
{
    public class Products
    {
        public Products()
        {
            this.ProductCategories = new HashSet<Categories>();
            this.Galleries = new HashSet<Gallery>();
            this.ProductAttributes = new HashSet<ProductAttribute>();
            this.Tags = new HashSet<Tags>();
            //this.Cart_Items = new HashSet<Cart_Items>();
            this.Variant_Options = new HashSet<variant_options>();
        }

        [Key]
        public Guid Id { get; set; }
        public string product_name { get; set; } = string.Empty;
        [Required]
        public string slug { get; set; } = string.Empty;
        public string? SKU { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal? sale_price { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        [Required]
        public decimal compare_price { get; set; }
        public int quantity { get; set; }
        public string short_description { get; set; } = string.Empty;
        [Column(TypeName = "TEXT")]
        public string product_description { get; set; } = string.Empty;
        public bool published { get; set; } = false;
        public string product_type { get; set; } = string.Empty;
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? update_at { get; set; } = DateTime.Now;
        public Guid created_by { get; set; }
        public Guid updated_by { get; set; }

        public virtual ICollection<Categories> ProductCategories { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }

        public virtual ICollection<variant_options> Variant_Options { get; set; }

        public virtual ICollection<Cart_Items> Cart_Items { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}