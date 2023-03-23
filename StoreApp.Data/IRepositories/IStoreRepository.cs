﻿using StoreApp.Domain.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.IRepositories
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
    }
}
