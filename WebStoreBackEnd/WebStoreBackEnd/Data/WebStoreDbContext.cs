using Microsoft.EntityFrameworkCore;
using WebStoreBackEnd.Models;

namespace WebStoreBackEnd.Data
{
    public class WebStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public WebStoreDbContext(DbContextOptions<WebStoreDbContext> options) : base(options)
        {
        }
    }
}
