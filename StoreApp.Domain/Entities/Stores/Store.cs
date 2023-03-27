using StoreApp.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Stores
{
    public class Store : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

    }
}
