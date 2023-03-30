using StoreApp.Domain.Entities.Stores;

namespace StoreApp.Service.Interfaces
{
    public interface ITabControlService
    {
        Task<TabController> Create(TabController model);

        Task<TabController> Update(TabController model);

        Task<bool> Delete(long id);

        Task<IList<TabController>> GetAll();

        Task<TabController> Get(long id);

        Task<bool> IsExist(string name);
    }
}
