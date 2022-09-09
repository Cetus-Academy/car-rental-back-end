using NetCoreTemplate.Domain.Reports.Entities;

namespace NetCoreTemplate.Application.Repositories;

public interface IReportRepository
{
    Task<Report> GetAsync(long reportId);
    Task CreateAsync(Report report);
    Task UpdateAsync(Report report);
    Task DeleteAsync(Report report);
}