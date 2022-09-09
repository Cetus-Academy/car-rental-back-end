using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreTemplate.Domain.Identity.Entities;

namespace NetCoreTemplate.Persistence.EF.Configurations.ReadConfig;

public class RoleEntityReadConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> b)
    {
        // Each Role can have many entries in the UserRole join table
        b.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // // Each Role can have many associated RoleClaims
        // b.HasMany(e => e.)
        //     .WithOne(e => e.Role)
        //     .HasForeignKey(rc => rc.RoleId)
        //     .IsRequired();
    }
}