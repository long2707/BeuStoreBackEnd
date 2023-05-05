namespace BeuStoreApi.Models.CategoriesModel
{
    public class CategoriesDTO
    {
        public Guid id { get; set; }
        public Guid? ParentId { get; set; }
        public string category_Name { get; set; }

        public virtual CategoriesDTO ParentMenu { get; set; }
        public virtual ICollection<CategoriesDTO> ChildCategories { get; set; }

    }
}
