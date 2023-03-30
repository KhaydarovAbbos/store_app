using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<SubCategory> Create(SubCategoryViewModel model);

        Task<SubCategory> Update(SubCategory model);

        Task<bool> Delete(long id);

        Task<IList<SubCategory>> GetAll(long categoryId);

        Task<SubCategory> Get(long id);

        Task<bool> IsExist(string name);

        Task<bool> UpdateCategoryName(string name, long subcategoryId);
    }
}
