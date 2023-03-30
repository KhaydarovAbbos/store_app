using StoreApp.Domain.Commons;
using StoreApp.Domain.Entities.Stores;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Products
{
    public class StoreProduct : IAuditable
    {
        public long Id { get; set; }

        public string Barcode { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
        public long StoreId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(SubcategoryId))]
        public SubCategory SubCategory { get; set; }
        public long SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }

        
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        public double ArrivalPrice { get; set; }

        public double Price { get; set; }
        
        public double Quantity { get; set; }
    }
}
