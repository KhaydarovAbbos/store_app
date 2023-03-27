using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Stores;

namespace StoreApp.Data.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
    }
}
