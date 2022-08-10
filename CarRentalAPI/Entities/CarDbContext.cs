using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI.Entities
{
    public class CarDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=CarDb;Trusted_Connection=True;";
        public DbSet<Car> cars { get; set; }
        public DbSet<CarDetails> carDetails { get; set; }
        public DbSet<CarEquipment> carEquipment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(r => r.slug)
                .IsRequired()
                .HasMaxLength(75);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
