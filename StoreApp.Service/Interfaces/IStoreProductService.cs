using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface IStoreProductService
    {
        Task<StoreProduct> Create(StoreProductViewModel model);

        Task<StoreProduct> Update(StoreProduct model);

        Task<bool> Delete(long id);

        Task<StoreProduct> Get(long id);

        Task<IList<StoreProduct>> GetAll(long storeId, long subCategoryId);

        Task<IList<StoreProduct>> GetAll(long storeId);

    }
}
