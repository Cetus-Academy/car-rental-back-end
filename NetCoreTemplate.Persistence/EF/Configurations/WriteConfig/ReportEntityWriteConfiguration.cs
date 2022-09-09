using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreTemplate.Domain.Reports.Entities;

namespace NetCoreTemplate.Persistence.EF.Configurations.WriteConfig;

public class ReportEntityWriteConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        
        builder.OwnsOne(a => a.Localization)
            .Property(a => a.Latitude)
            .HasColumnName("Latitude");
        
        builder.OwnsOne(a => a.Localization)
            .Property(a => a.Longitude)
            .HasColumnName("Longitude");
        
        builder.OwnsOne(r => r.Address, opt =>
        {
            opt.Property(a => a.HouseNumber).HasColumnName("HouseNumber");
            opt.Property(a => a.Street).HasColumnName("Street");
            opt.Property(a => a.City).HasColumnName("City").IsRequired();
            opt.Property(a => a.PostalCode).HasColumnName("PostalCode");
        });

        builder.Property<string>("CreatedBy");
        builder.Property<DateTime>("CreatedAt");
        builder.Property<string>("LastModifiedBy");
        builder.Property<DateTime?>("LastModifiedAt");
        builder.Property<string>("InactivatedBy");
        builder.Property<DateTime>("InactivatedAt");
        builder.Property<short>("EntryStatus");
    }
}