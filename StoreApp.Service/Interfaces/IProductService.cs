using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;
using System.Diagnostics.SymbolStore;

namespace StoreApp.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> Create(ProductViewModel model);

        Task<Product> Update(Product model);

        Task<bool> Delete(long id);

        Task<IList<Product>> GetAll();

        Task<IList<Product>> GetProducts();

        Task<Product> Get(long id);
        Task<Product> Get(string barcode);

        Task<bool> IsExist(string name);
        Task<bool> IsExist(string name, long id);

        Task UpdateCategoryName(string name, long categoryId);
        Task UpdateSubcategoryname(string name, long subCategoryId);
    }
}
