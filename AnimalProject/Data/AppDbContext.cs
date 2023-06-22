using AnimalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines {get; set;}
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //modelBuilder.Entity<Medicine>()
            //    .HasOne(u => u.DonorUser)
            //    .WithMany(m => m.DonationsMade)
            //    .HasForeignKey(s => s.DonorUserId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Medicine>()
            //   .HasOne(u => u.DoneeUser)
            //   .WithMany(m => m.DonationsReceived)
            //   .HasForeignKey(s => s.DoneeUserId)
            //   .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Medicine>()
                .HasOne(u => u.DonorUser)
                .WithMany(m => m.Medicines)
                .HasForeignKey(s => s.DonorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Donation>()
                .HasOne(u => u.DoneeUser)
                .WithMany(m => m.DonationsReceived)
                .HasForeignKey(s => s.DoneeUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Donation>()
                .HasOne(u => u.DonorUser)
                .WithMany(m => m.DonationsMade)
                .HasForeignKey(s => s.DonorUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=NB384-MGMM;Database=AnimalProject;Trusted_Connection=True;");
        }

        
    }
}
