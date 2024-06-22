using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreBackEnd.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = default!;
        [MaxLength(255), Required]
        public string Email { get; set; } = null!;
        [MaxLength(255), Required]
        public string Password { get; set; } = null!;
    }
}
