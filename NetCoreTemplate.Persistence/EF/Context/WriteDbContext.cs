using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Domain.Common.Enums;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Domain.Reports.Entities;
using NetCoreTemplate.Persistence.EF.Configurations.ReadConfig;
using NetCoreTemplate.Persistence.EF.Configurations.WriteConfig;
using NetCoreTemplate.Persistence.EF.Models;

namespace NetCoreTemplate.Persistence.EF.Context;

public class WriteDbContext : IdentityDbContext<User, Role, long, UserClaim, UserRole, IdentityUserLogin<long>,
    IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    private readonly IDateService _dateTimeOffset;
    private readonly ICurrentUserService _currentUserService;

    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    public WriteDbContext(DbContextOptions<WriteDbContext> options, IDateService dateTimeOffset,
        ICurrentUserService currentUserService) : base(options)
    {
        _dateTimeOffset = dateTimeOffset;
        _currentUserService = currentUserService;
    }

    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<User>(new UserEntityReadConfiguration());
        modelBuilder.ApplyConfiguration<Role>(new RoleEntityReadConfiguration());
        
        modelBuilder.ApplyConfiguration<Report>(new ReportEntityWriteConfiguration());

        base.OnModelCreating(modelBuilder);
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Report or)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedBy"] = _currentUserService.Email;
                        entry.CurrentValues["CreatedAt"] = _dateTimeOffset.CurrentDate();
                        entry.CurrentValues["EntryStatus"] = EntryStatus.Active;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues["LastModifiedBy"] = _currentUserService.Email;
                        entry.CurrentValues["LastModifiedAt"] = _dateTimeOffset.CurrentDate();
                        break;

                    case EntityState.Deleted:
                        entry.CurrentValues["LastModifiedBy"] = _currentUserService.Email;
                        entry.CurrentValues["LastModifiedAt"] = _dateTimeOffset.CurrentDate();
                        entry.CurrentValues["InactivatedBy"] = _currentUserService.Email;
                        entry.CurrentValues["InactivatedAt"] = _dateTimeOffset.CurrentDate();
                        entry.CurrentValues["EntryStatus"] = EntryStatus.Deleted;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}