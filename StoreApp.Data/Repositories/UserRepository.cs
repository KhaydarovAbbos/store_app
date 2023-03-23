using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

    }
}
