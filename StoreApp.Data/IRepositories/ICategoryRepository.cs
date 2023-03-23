using StoreApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
