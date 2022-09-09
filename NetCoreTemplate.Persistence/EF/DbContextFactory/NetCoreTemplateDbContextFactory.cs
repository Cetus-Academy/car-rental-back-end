using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Persistence.EF.Context;

namespace NetCoreTemplate.Persistence.EF.DbContextFactory;

public class NetCoreTemplateDbContextFactory : DesignTimeDbContextFactoryBase<ReadDbContext>
{
    protected override ReadDbContext CreateNewInstance(DbContextOptions<ReadDbContext> options)
    {
        return new ReadDbContext(options);
    }
}