using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface ICashService
    {
        Task<Cash> Create(CashViewModel model);

        Task<Cash> Update(Cash model);

        Task<bool> Delete(long id);

        Task<IList<Cash>> GetAll(long storeId);

        Task<Cash> Get(long id);

        Task<bool> IsExist(string name, long storeId);

        Task UpdateStoreName(string name, long storeId);
    }
}
