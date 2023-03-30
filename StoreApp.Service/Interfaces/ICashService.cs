using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface ICashService
    {
        Task<Cash> Create(CashViewModel model);

        Task<Cash> Update(Cash model);

        Task<bool> Delete(long id);

        Task<IList<Cash>> GetAll(long storeId);

        Task<Cash> Get(long id);

        Task<bool> IsExist(string name);

        Task UpdateStoreName(string name, long storeId);
    }
}
