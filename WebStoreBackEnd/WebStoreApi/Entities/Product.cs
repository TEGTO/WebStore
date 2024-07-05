using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreApi.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        [MaxLength(256)]
        public string? Name { get; set; }
        public float Price { get; set; }
        public float AvgRating { get; set; }
        [MaxLength(256)]
        public string? ImgUrl { get; set; }
        public List<UserProduct> UserProducts { get; set; } = default!;
    }
}
