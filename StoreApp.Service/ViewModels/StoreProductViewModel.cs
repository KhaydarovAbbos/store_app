using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.ViewModels
{
    public class StoreProductViewModel
    {
        public long StoreId { get; set; }
        public long SubcategoryId { get; set; }

        public long ProductId { get; set; }

        public double Quantity { get; set; }
    }
}
