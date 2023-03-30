using StoreApp.Domain.Commons;
using StoreApp.Domain.Entities.Stores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Domain.Entities.Products
{
    public class StoreProduct : IAuditable
    {
        [Column("Id", Order = 1)]
        public long Id { get; set; }
        [Column("Barcode", Order = 2)]
        public string Barcode { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
        [Column("StoreId", Order = 3)]
        public long StoreId { get; set; }

        [Column("StoreName", Order = 4)]
        public string StoreName { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Column("CategoryId", Order = 5)]
        public long CategoryId { get; set; }

        [Column("CategoryName", Order = 6)]
        public string CategoryName { get; set; }


        [ForeignKey(nameof(SubcategoryId))]
        public SubCategory SubCategory { get; set; }

        [Column("SubcategoryId", Order = 7)]
        public long SubcategoryId { get; set; }

        [Column("SubcategoryName", Order = 8)]
        public string SubcategoryName { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Column("ProductId", Order = 9)]
        public long ProductId { get; set; }

        [Column("ProductName", Order = 10)]
        public string ProductName { get; set; }

        [Column("ArrivalPrice", Order = 11)]
        public double ArrivalPrice { get; set; }

        [Column("Price", Order = 12)]
        public double Price { get; set; }
        [Column("Quantity", Order = 13)]
        public double Quantity { get; set; }
    }
}
