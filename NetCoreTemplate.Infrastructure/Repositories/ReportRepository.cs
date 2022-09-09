using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Repositories;
using NetCoreTemplate.Domain.Reports.Entities;
using NetCoreTemplate.Persistence;
using NetCoreTemplate.Persistence.EF.Context;

namespace NetCoreTemplate.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly WriteDbContext _db;

    public ReportRepository(WriteDbContext db)
    {
        _db = db;
    }
    
    public async Task<Report> GetAsync(long reportId)
    {
        return await _db.Reports
            .FirstOrDefaultAsync(a => a.Id == reportId); 
    }

    public async Task CreateAsync(Report report)
    {
        await _db.Reports.AddAsync(report);
        await _db.SaveChangesAsync();

    }

    public async Task UpdateAsync(Report report)
    {
        _db.Reports.Update(report);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Report report)
    {
        _db.Reports.Remove(report);
        await _db.SaveChangesAsync();
    }
}