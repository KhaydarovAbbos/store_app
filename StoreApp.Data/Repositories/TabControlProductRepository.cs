using StoreApp.Data.IRepositories;
using StoreApp.Domain.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Repositories
{
    public class TabControlProductRepository : GenericRepository<TabControlProduct>, ITabControlProductRepository
    {
    }
}
