using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface ITabControlService
    {
        Task<TabController> Create(TabController model);

        Task<TabController> Update(TabController model);

        Task<bool> Delete(long id);

        Task<IList<TabController>> GetAll(long storeId);

        Task<TabController> Get(long id);

        Task<bool> IsExist(string name);
    }
}
