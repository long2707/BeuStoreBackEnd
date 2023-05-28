namespace BeuStoreApi.Models.CategoriesModel
{
    public class CategoriesDTO
    {
        public Guid id { get; set; }
        public Guid? ParentId { get; set; }
        public string category_Name { get; set; }

        public string category_Description { get; set; } 
        public virtual ICollection<CategoriesDTO> ChildCategories { get; set; }

    }
}
