using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Domain.Common.Entities;
using NetCoreTemplate.Domain.Common.Enums;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Persistence.EF.Configurations.ReadConfig;
using NetCoreTemplate.Persistence.EF.Models;

namespace NetCoreTemplate.Persistence.EF.Context;

public class ReadDbContext : IdentityDbContext<User, Role, long, UserClaim, UserRole, IdentityUserLogin<long>,
    IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public DbSet<ReportReadModel> Reports { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration<User>(new UserEntityReadConfiguration());
        modelBuilder.ApplyConfiguration<Role>(new RoleEntityReadConfiguration());
        
        modelBuilder.ApplyConfiguration(new ReportEntityReadConfiguration()).Entity<ReportReadModel>()
            .HasQueryFilter(p => p.EntryStatus != EntryStatus.Deleted);
        
    }

    
}