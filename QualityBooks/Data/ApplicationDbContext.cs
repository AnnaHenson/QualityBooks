using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualityBooks.Areas.ShoppingCart.Models;
using QualityBooks.Models;

namespace QualityBooks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Book>().ToTable("Book");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Supplier>().ToTable("Supplier");
            builder.Entity<OrderDetail>().ToTable("OrderDetail");

            builder.Entity<OrderDetail>().HasOne(p =>p.Order).WithMany(o =>o.OrderDetails).OnDelete(DeleteBehavior.Cascade);

            
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
