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
    public interface ICategoryService
    {
        Task<Category> Create(CategoryViewModel model);

        Task<Category> Update(Category model);

        Task<bool> Delete(long id);

        Task<IList<Category>> GetAll();

        Task<Category> Get(long id);

    }
}
