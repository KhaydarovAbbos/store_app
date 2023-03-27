using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Products;

namespace StoreApp.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
