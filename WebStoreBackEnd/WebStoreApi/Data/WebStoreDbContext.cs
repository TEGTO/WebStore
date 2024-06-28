using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebStoreApi.Models;

namespace WebStoreApi.Data
{
    public class WebStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public IConfiguration Configuration { get; }

        public WebStoreDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema( "store");
        }
    }
}
