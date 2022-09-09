using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreTemplate.Persistence.EF.Models;

namespace NetCoreTemplate.Persistence.EF.Configurations.ReadConfig;

public class ReportEntityReadConfiguration : IEntityTypeConfiguration<ReportReadModel>
{
    public void Configure(EntityTypeBuilder<ReportReadModel> builder)
    {
        builder.HasKey(pl => pl.Id);
        
        builder.OwnsOne(r => r.Address, opt =>
        {
            opt.Property(a => a.HouseNumber).HasColumnName("HouseNumber");
            opt.Property(a => a.Street).HasColumnName("Street");
            opt.Property(a => a.City).HasColumnName("City").IsRequired();
            opt.Property(a => a.PostalCode).HasColumnName("PostalCode");
        });
        
        builder.OwnsOne(a => a.Localization)
            .Property(a => a.Latitude)
            .HasColumnName("Latitude");
        
        builder.OwnsOne(a => a.Localization)
            .Property(a => a.Longitude)
            .HasColumnName("Longitude");
    }
}