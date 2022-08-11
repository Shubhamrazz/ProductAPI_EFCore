using Microsoft.EntityFrameworkCore;
using Repository_Layer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();
        }
    }
}
