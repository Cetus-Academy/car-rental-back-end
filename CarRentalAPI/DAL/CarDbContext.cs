using CarRentalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.DAL;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base()
    {

    }
    public const string _connectionString =
        "Server=(localdb)\\mssqllocaldb;Database=CarDb;Trusted_Connection=True;";
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarEquipment> CarEquipment { get; set; }
    public DbSet<CarBrand> CarBrand { get; set; }
    public DbSet<Image> Image { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .Property(r => r.Slug)
            .IsRequired()
            .HasMaxLength(75);

        //TODO: add more db row limits
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}