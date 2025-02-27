﻿using Microsoft.EntityFrameworkCore;
using WebStoreApi.Domain.Entities;

namespace WebStoreApi.Data
{
    public class WebStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        public WebStoreDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
