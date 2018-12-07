using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Refferal> Refferals { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                 .HasOne(p => p.Patient)
                 .WithOne(i => i.Card);

            modelBuilder.Entity<Card>()
                 .HasMany(b => b.Therapies)
                     .WithOne();

            modelBuilder.Entity<Card>()
              .HasMany(b => b.Prescriptions)
                  .WithOne();

            modelBuilder.Entity<Card>()
              .HasMany(b => b.Refferals)
                  .WithOne();

            modelBuilder.Entity<Card>()
              .HasMany(b => b.Allergens)
                  .WithOne();
        }
    }
}
