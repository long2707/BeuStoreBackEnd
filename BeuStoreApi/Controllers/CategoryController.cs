using BeuStoreApi.Models;
using BeuStoreApi.Models.CategoriesModel;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategories _categories;
        public CategoryController(ICategories categories) 
        
        { 
            _categories = categories;
        }

        [HttpGet("categories")]
        //[Authorize]
        public async Task<statusDTO> GetAllAsync()
        {
            return await _categories.GetAllCategory();
        }
        [HttpPost("create-category"), Authorize(Roles ="admin")]
        
        public async Task<statusDTO> saveCategory([FromBody] SaveCategoryDTO saveCategory, Guid? parentId)
        {
            return await _categories.SaveCategoryAsync(saveCategory, parentId);
        }

        [HttpPut("update-category"), Authorize(Roles ="admin")]
       // [Authorize(Roles = "admin")]
        public async Task<statusDTO> updateCategory([FromBody] SaveCategoryDTO saveCategory, Guid id, Guid? parentId)
        {
            return await _categories.updateCategoryAsync(saveCategory, id, parentId);
                 
        }
        [HttpDelete("delete-category")]
       // [Authorize(Roles = "admin")]
        public async Task<statusDTO> deleteCategory(Guid id)
        {
            return await _categories.DeleteAsync(id);
        }
    }
}
