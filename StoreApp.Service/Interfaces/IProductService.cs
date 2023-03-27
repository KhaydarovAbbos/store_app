using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;

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

        Task<bool> IsExist(string name);
        Task<bool> IsExist(string name, long id);

    }
}
