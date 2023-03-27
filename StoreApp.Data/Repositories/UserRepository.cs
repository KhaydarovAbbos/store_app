using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Users;

namespace StoreApp.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

    }
}
