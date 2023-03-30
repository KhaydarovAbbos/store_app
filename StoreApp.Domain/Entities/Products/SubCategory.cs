using StoreApp.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Products
{
    public class SubCategory : IAuditable
    {
        public long Id { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

    }
}
