using StoreApp.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Products
{
    public class Product : IAuditable
    {
        public long Id { get; set; }
        public string Barcode { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        
        
        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory SubCategory { get; set; }
        public long SubCategoryId { get; set; }


        public string Name { get; set; }

        public double ArrivalPrice { get; set; }

        public double Price { get; set; }
    }
}
