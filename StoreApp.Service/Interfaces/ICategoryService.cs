using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> Create(CategoryViewModel model);

        Task<Category> Update(Category model);

        Task<bool> Delete(long id);

        Task<IList<Category>> GetAll();

        Task<Category> Get(long id);

        Task<bool> IsExist(string name);

    }
}
