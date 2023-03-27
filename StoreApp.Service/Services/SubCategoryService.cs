using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
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

            var subcategory = await Get(id);

            if (subcategory != null)
            {
                await SubCategoryRepository.DeleteAsync(x => x.Id == subcategory.Id);

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

        public async Task<IList<SubCategory>> GetAll(long categoryId)
        {
            var subcategories = await SubCategoryRepository.GetAllAsync(x => x.CategoryId == categoryId);

            return subcategories.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<SubCategory> Update(SubCategory model)
        {
            var existSubcategory = await Get(model.Id);

            if (existSubcategory == null)
            {
                return existSubcategory;
            }
            else
            {
                existSubcategory.Name = model.Name;

                return await SubCategoryRepository.UpdateAsync(existSubcategory);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            var isExistSubCategory = await SubCategoryRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return isExistSubCategory == null ? false : true;
        }
    }
}
