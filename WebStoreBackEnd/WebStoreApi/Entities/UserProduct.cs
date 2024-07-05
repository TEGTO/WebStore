using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreApi.Entities
{
    public class UserProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public int ProductId { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
