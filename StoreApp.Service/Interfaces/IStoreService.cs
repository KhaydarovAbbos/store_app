using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
