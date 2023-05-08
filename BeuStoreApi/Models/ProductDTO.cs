using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Models
{
    public class ProductDTO
    {
        public string product_name { get; set; }
        public string SKU { get; set; }
        [Required]
        public decimal regular_price { get; set; }
       
        public decimal discount_price { get; set; }
        [Required]
        public int quantity { get; set; }

        public string product_description { get; set; }
        public Guid createed_by { get; set; }
        public ICollection<FormFile>? thumbails { get; set; }

    }
}
