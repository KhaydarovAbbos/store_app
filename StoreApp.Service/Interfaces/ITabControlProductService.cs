using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface ITabControlProductService
    {
        Task<TabControlProduct> Create(TabControlProductViewModel model);

        Task<TabControlProduct> Update(TabControlProduct model);

        Task<bool> Delete(long id);

        Task<IList<TabControlProduct>> GetAll(long storeId);

        Task<TabControlProduct> Get(long id);

        Task<bool> IsExist(string name);
    }
}
