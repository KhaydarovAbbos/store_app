using StoreApp.Domain.Entities.Products;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<SubCategory> Create(SubCategoryViewModel model);

        Task<SubCategory> Update(SubCategory model);

        Task<bool> Delete(long id);

        Task<IList<SubCategory>> GetAll(long categoryId);

        Task<SubCategory> Get(long id);

        Task<bool> IsExist(string name);
    }
}
