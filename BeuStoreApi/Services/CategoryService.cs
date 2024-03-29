﻿using AutoMapper;
using BeuStoreApi.Entities;
using BeuStoreApi.Models;
using BeuStoreApi.Models.CategoriesModel;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeuStoreApi.Services
{
    public class CategoryService : ICategories
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryService(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var categoryExits = _dbContext.categories.FirstOrDefault(item => item.categoryId == id);
            if (categoryExits != null)
            {
                var parentCategories = _dbContext.categories.Where(x => x.parent_id == id).ToList();
                if (parentCategories.Count() > 0)
                {
                    foreach (var category in parentCategories)
                    {
                        category.parent_id = null;
                    }
                }

                _dbContext.Remove(categoryExits);
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult(new { messages = "Xóa danh mục thành công" });

            }
            return new OkObjectResult(new { messages = "danh mục không tồn tại" })
            {

                StatusCode = 400
            };

        }

        public async Task<IActionResult> GetAllCategory()
        {
            var menuItems = await _dbContext.categories.Include(x => x.Children).ToListAsync();
            var rootMenus = menuItems.Where(x => x.parent_id == null).ToList();
            foreach (var item in rootMenus)
            {
                item.Children = BuildCategoriesTree(item.categoryId, menuItems);
            }
            var result = _mapper.Map<IEnumerable<Categories>, IEnumerable<CategoriesDTO>>(rootMenus);
            return new OkObjectResult(new { data = result });
        }


        public async Task<IActionResult> SaveCategoryAsync(SaveCategoryDTO saveCategory, Guid? parentId)
        {
            var categoryExits = _dbContext.categories.Where(item => item.categoryId == parentId).FirstOrDefault();
            var createCategory = new Categories()
            {
                categoryId = new Guid(),
                category_Name = saveCategory.category_Name,
                category_Description = saveCategory.category_Description

            };
            if (categoryExits != null)
            {
                createCategory.parent_id = parentId;


            }
            _dbContext.Add(createCategory);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(new { messages = "Tạo danh mục thành công" });

        }

        public async Task<IActionResult> updateCategoryAsync(SaveCategoryDTO saveCategoryDTO, Guid id, Guid? parentid)
        {
            var categoryExits = _dbContext.categories.FirstOrDefault(item => item.categoryId == id);
            if (categoryExits != null)
            {
                categoryExits.parent_id = parentid;
                categoryExits.category_Name = saveCategoryDTO.category_Name;
                categoryExits.category_Description = saveCategoryDTO.category_Description;

                await _dbContext.SaveChangesAsync();

                return new OkObjectResult(new { messages = "Cập nhật danh mục thành công" });

            }

            return new ObjectResult(new { messages = "Danh mục chưa được tạo" })
            {
                StatusCode = 400
            };

        }

        private ICollection<Categories> BuildCategoriesTree(Guid id, List<Categories> categories)
        {
            var menus = categories.Where(item => item.parent_id == id);
            foreach (var menu in menus)
            {
                menu.Children = BuildCategoriesTree(menu.categoryId, categories);
            }
            return menus.ToList();
        }


    }
}