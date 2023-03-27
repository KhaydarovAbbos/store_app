using StoreApp.Domain.Commons;

namespace StoreApp.Domain.Entities.Products
{
    public class Category : IAuditable
    {
        public long Id { get; set; }

        public string Name { get; set; }

    }
}
