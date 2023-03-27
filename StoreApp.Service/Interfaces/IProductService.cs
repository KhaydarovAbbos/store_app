using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
