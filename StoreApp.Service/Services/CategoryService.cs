﻿using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository { get; set; }

        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }

        public async Task<Category> Create(CategoryViewModel model)
        {

            Category category = new Category()
            {
                Name = model.Name
            };

            return await categoryRepository.CreatAsync(category);

        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var category = await Get(id);

            if (category != null)
            {
                await categoryRepository.DeleteAsync(x => x.Id == category.Id);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<Category> Get(long id)
        {
            return await categoryRepository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<Category>> GetAll()
        {
            var categories = await categoryRepository.GetAllAsync();

            return categories.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Category> Update(Category model)
        {
            var existCategory = await Get(model.Id);

            if (existCategory == null)
            {
                return existCategory;
            }
            else
            {
                existCategory.Name = model.Name;

                return await categoryRepository.UpdateAsync(existCategory);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            var isExistCategory = await categoryRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return isExistCategory == null ? false : true;

        }
    }
}
