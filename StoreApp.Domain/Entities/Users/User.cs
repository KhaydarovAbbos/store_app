using StoreApp.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities.Users
{
    public class User : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }
}
