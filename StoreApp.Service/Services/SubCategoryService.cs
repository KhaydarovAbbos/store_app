using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Enums;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        ISubCategoryRepository SubCategoryRepository { get; set; }

        public SubCategoryService()
        {
            SubCategoryRepository = new SubCategoryRepository();
        }

        public async Task<SubCategory> Create(SubCategoryViewModel model)
        {
            SubCategory category = new SubCategory()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                
            };

            return await SubCategoryRepository.CreatAsync(category);
        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var store = await Get(id);

            if (store != null)
            {
                await SubCategoryRepository.UpdateAsync(store);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<SubCategory> Get(long id)
        {
            return await SubCategoryRepository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<SubCategory>> GetAll()
        {
            var stores = await SubCategoryRepository.GetAllAsync();

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<SubCategory> Update(SubCategory model)
        {
            var existStore = await Get(model.Id);

            if (existStore == null)
            {
                return existStore;
            }
            else
            {
                existStore.Name = model.Name;

                return await SubCategoryRepository.UpdateAsync(existStore);
            }
        }
    }
}
