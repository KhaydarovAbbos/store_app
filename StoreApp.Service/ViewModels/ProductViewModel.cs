using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public double Arrivalprice { get; set; }

        public double Price { get; set; }

        public double Quantity { get; set; }

        public string Barcode { get; set; }

        public long SubCategoryId { get; set; }

        public long StoreId { get; set; }

    }
}
