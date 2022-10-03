using CarRentalAPI.Common;
using CarRentalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.DAL;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base()
    {
    }

    private static string _connectionString = DatabaseSettings.DbConnectionString;

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarEquipment> CarEquipment { get; set; }
    public DbSet<CarBrand> CarBrand { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<Reservations> Reservations { get; set; }
    public DbSet<CarReservations> CarReservations { get; set; }
    public DbSet<CarLocation> CarLocations { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .Property(r => r.Slug)
            .IsRequired()
            .HasMaxLength(75);

        modelBuilder.Entity<Car>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(76);

        modelBuilder.Entity<Product>()
            .Property(r => r.Slug)
            .IsRequired()
            .HasMaxLength(40);

        modelBuilder.Entity<Product>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(41);

        //TODO: add more db row limits
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
