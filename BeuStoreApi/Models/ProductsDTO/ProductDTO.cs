using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Models.ProductsDTO
{
    public class ProductDTO
    {
        public string product_name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string product_description { get; set; } = string.Empty;
        [Required]
        public decimal regular_price { get; set; }
        public decimal? discount_price { get; set; }
        [Required]
        public int quantity { get; set; }
        public string[] tags { get; set; } = { };
        public Guid[] categories { get; set; } = { };
        public IList<AttributeAndValueDTO>? attribute { get; set; }
        // public string 
        public Guid created_by { get; set; }
        public ICollection<IFormFile>? thumbails { get; set; }


    }
}
