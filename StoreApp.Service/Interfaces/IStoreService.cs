using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface IStoreService
    {
        Task<Store> Create(StoreViewModel model);

        Task<Store> Update(Store model);

        Task<bool> Delete(long id);

        Task<IList<Store>> GetAll();

        Task<Store> Get(long id);

        Task<bool> IsExist(string name);
    }
}
