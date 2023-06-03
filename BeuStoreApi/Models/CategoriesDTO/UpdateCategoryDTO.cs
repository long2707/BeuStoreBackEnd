namespace BeuStoreApi.Models.CategoriesDTO
{
    public class UpdateCategoryDTO
    {
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; }= string.Empty;
    }
}
