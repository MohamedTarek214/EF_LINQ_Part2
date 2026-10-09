using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Context
{
    internal class ECommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ECommerceDB;Integrated Security=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name).IsRequired().HasMaxLength(100);
                e.Property(p => p.Price).HasColumnType("decimal(10,2)");

                e.HasOne(p => p.Categorys)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(c => c.Name).IsRequired().HasMaxLength(150);
                e.Property(c => c.Email).IsRequired().HasMaxLength(255);
                e.HasIndex(c => c.Email).IsUnique();
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(o => o.Customers)
                 .WithMany(c => c.Orders)
                 .HasForeignKey(o => o.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.HasKey(od => new { od.OrderId, od.ProductId });

                e.Property(od => od.Quantity).IsRequired();

                e.HasOne(od => od.Order)
                 .WithMany(o => o.OrderDetails)
                 .HasForeignKey(od => od.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(od => od.Product)
                 .WithMany(p => p.OrderDetails)
                 .HasForeignKey(od => od.ProductId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
