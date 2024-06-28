using AuthenticationWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace AuthenticationWebApi.Data
{
    public class AuthIdentityDbContext : IdentityDbContext<User>
    {
        public IConfiguration Configuration { get; }

        public AuthIdentityDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Configuration["DB_SCHEMA"] ?? "public");
            base.OnModelCreating(builder);
            builder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
