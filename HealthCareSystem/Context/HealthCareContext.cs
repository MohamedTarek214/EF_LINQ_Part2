using HealthCareSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Context
{
    internal class HealthCareContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HealthCareDB;Integrated Security=True;Trust Server Certificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(e =>
            {
                e.Property(p => p.Name).IsRequired().HasMaxLength(150);
                e.Property(p => p.DateOfBirth).IsRequired();
            });

            modelBuilder.Entity<Doctor>(e =>
            {
                e.Property(d => d.Name).IsRequired().HasMaxLength(150);
                e.Property(d => d.Specialization).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Appointment>(e =>
            {
                e.HasKey(a => new { a.PatientId, a.DoctorId, a.AppointmentDate });

                e.Property(a => a.AppointmentDate).IsRequired();

                e.HasOne(a => a.Patient)
                 .WithMany(p => p.Appointments)
                 .HasForeignKey(a => a.PatientId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(a => a.Doctor)
                 .WithMany(d => d.Appointments)
                 .HasForeignKey(a => a.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(a => a.DoctorId);
            });
        }
    }
}
