using AuthenticationWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebStoreApi.Models
{
    [Keyless]
    public class UserProduct
    {
        public string UserId { get; set; } = default!;
        public int ProductId { get; set; } = default!;
        public User User { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
