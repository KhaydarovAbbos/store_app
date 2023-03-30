using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Stores
{
    public class Cash
    {
        public long Id { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        public long StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

    }
}
