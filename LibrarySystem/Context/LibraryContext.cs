using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Context
{
    internal class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=LibraryDB;Integrated Security=True;Trust Server Certificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(e =>
            {
                e.Property(a => a.Name).IsRequired().HasMaxLength(100);
                e.Property(a => a.BirthDate).IsRequired();
            });

            modelBuilder.Entity<Book>(e =>
            {
                e.Property(b => b.Title).IsRequired().HasMaxLength(200);

                e.Property(b => b.ISBN).IsRequired().HasMaxLength(15);
                e.HasIndex(b => b.ISBN).IsUnique();

                e.HasOne(b => b.Author)
                 .WithMany(a => a.Books)
                 .HasForeignKey(b => b.AuthorId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Borrower>(e =>
            {
                e.Property(b => b.Name).IsRequired().HasMaxLength(150);
                e.Property(b => b.MembershipDate).IsRequired();
            });

            modelBuilder.Entity<Loan>(e =>
            {
                e.HasKey(l => new { l.BookId, l.BorrowerId, l.LoanDate });

                e.Property(l => l.LoanDate).IsRequired();
                
                e.HasOne(l => l.Book)
                 .WithMany(b => b.Loans)
                 .HasForeignKey(l => l.BookId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(l => l.Borrower)
                 .WithMany(b => b.Loans)
                 .HasForeignKey(l => l.BorrowerId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
