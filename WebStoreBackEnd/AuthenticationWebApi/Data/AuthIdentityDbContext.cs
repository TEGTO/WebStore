using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthenticationWebApi.Models;

namespace AuthenticationWebApi.Data
{
    public class AuthIdentityDbContext : IdentityDbContext<User>
    {
        public AuthIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
