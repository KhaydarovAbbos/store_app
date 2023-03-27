using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Products;

namespace StoreApp.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
    }
}
