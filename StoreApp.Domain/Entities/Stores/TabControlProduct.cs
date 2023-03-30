using StoreApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Entities.Stores
{
    public class TabControlProduct
    {
        public long Id { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        [ForeignKey(nameof(TabControllerId))]
        public TabController TabController { get; set; }
        public long TabControllerId { get; set; }

        public string TabControllerName { get; set; }
    }
}
