using BeuStoreApi.Models;
using BeuStoreApi.Models.CategoriesModel;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Services.interfaces
{
    public interface ICategories
    {
        Task<IActionResult> GetAllCategory();
        Task<IActionResult> SaveCategoryAsync(SaveCategoryDTO saveCategory, Guid? parentId);
        Task<IActionResult> updateCategoryAsync(SaveCategoryDTO saveCategoryDTO, Guid id, Guid? parentid);
        Task<IActionResult> DeleteAsync(Guid id);
        //Task<statusDTO> GetByIdAsync(Guid id);

    }
}
