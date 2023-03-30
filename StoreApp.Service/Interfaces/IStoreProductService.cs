using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface IStoreProductService
    {
        Task<StoreProduct> Create(StoreProductViewModel model);

        Task<StoreProduct> Update(StoreProduct model);

        Task<bool> Delete(long id);

        Task<StoreProduct> Get(long id);

        Task<StoreProduct> Get(long id, string barcode);

        Task<StoreProduct> Get(long productId, long storeId);

        Task<IList<StoreProduct>> GetAll(long storeId, long subCategoryId);

        Task<IList<StoreProduct>> GetAll(long storeId);

        Task UpdateCategoryName(string name, long categoryId);
        Task UpdateStoreName(string name, long storeId);
    }
}
