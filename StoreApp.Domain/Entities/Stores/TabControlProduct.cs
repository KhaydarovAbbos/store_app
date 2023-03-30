using StoreApp.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Stores
{
    public class TabControlProduct
    {
        public long Id { get; set; }

        [ForeignKey(nameof(TabControllerId))]
        public TabController TabController { get; set; }
        public long TabControllerId { get; set; }

        public string TabControllerName { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
