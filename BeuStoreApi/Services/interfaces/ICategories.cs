using BeuStoreApi.Models;
using BeuStoreApi.Models.CategoriesModel;

namespace BeuStoreApi.Services.interfaces
{
    public interface ICategories
    {
        Task<statusDTO> GetAll();
        Task<statusDTO> SaveCategoryAsync( SaveCategoryDTO saveCategory, Guid? parentId);
        Task<statusDTO> updateCategoryAsync(SaveCategoryDTO saveCategoryDTO, Guid id, Guid? parentid);
        Task<statusDTO> DeleteAsync(Guid id);
        //Task<statusDTO> GetByIdAsync(Guid id);

    }
}
