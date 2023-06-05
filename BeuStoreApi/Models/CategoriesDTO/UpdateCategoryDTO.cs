using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Models.CategoriesDTO
{
    
    public class UpdateCategoryDTO
    {
        [FromForm]
        public Guid? CategoryId { get; set; }
        [FromForm]
        public string CategoryName { get; set; } = string.Empty;
        [FromForm]
        public string CategoryDescription { get; set; }= string.Empty;
    }
}
