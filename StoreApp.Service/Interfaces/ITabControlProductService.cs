using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface ITabControlProductService
    {
        Task<TabControlProduct> Create(TabControlProductViewModel model);

        Task<TabControlProduct> Update(TabControlProduct model);

        Task<bool> Delete(long id);

        Task<IList<TabControlProduct>> GetAll(long controlId);

        Task<TabControlProduct> Get(long id);

        Task<bool> IsExist(string name, long controlId);

        Task UpdateProductName(string name, long productId);
    }
}
