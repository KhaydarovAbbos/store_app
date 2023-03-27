using StoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Commons
{
    public interface IAuditable
    {
        public long Id { get; set; }

    }
}
