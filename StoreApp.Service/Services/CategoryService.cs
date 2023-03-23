using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Enums;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            category.Create();

            return await categoryRepository.CreatAsync(category);

        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var store = await Get(id);

            if (store != null)
            {
                store.Delete();

                await categoryRepository.UpdateAsync(store);

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
            return await categoryRepository.GetAsync(x => x.Id == id && x.State != ItemState.Deleted);
        }

        public async Task<IList<Category>> GetAll()
        {
            var stores = await categoryRepository.GetAllAsync(x => x.State != ItemState.Deleted);

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Category> Update(Category model)
        {
            var existStore = await Get(model.Id);

            if (existStore == null)
            {
                return existStore;
            }
            else
            {
                existStore.Name = model.Name;
                existStore.Update();

                return await categoryRepository.UpdateAsync(existStore);
            }
        }
    }
}
