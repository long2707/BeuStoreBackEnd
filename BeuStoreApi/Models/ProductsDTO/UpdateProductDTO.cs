using BeuStoreApi.Models.CategoriesDTO;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Models.ProductsDTO
{
    public class UpdateProductDTO
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
        public ICollection<UpdateCategoryDTO> updateCategories { get; set; }
        public IList<UpdateAttributeDTO> updateAttributes { get; set; }
        public Guid created_by { get; set; }

    }

    
}
