using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Models
{
    public class ProductDTO
    {
        public string product_name { get; set; }
        public string SKU { get; set; }
        public string product_description { get; set; }
        [Required]
        public decimal regular_price { get; set; }
        public decimal? discount_price { get; set; }
        [Required]
        public int quantity { get; set; }
        public string[] tags { get; set; }
        public string[] categories { get; set; }
        public Guid created_by { get; set; }
       // public ICollection<FormFile>? thumbails { get; set; }

    }
}
