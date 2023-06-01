using BeuStoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Models.CategoriesModel
{
    public class CategoriesDTO
    {
        public Guid categoryId { get; set; }
        public Guid? parent_id { get; set; }
        public string category_Name { get; set; } = string.Empty;

        public string category_Description { get; set; } = string.Empty;
        public virtual ICollection<CategoriesDTO>? Children { get; set; }
       // public virtual ICollection<Products> Products { get; set; } 



    }
}
