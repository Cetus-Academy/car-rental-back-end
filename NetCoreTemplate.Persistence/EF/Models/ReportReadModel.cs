using NetCoreTemplate.Domain.Common.Entities;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Domain.Reports.Enums;
using NetCoreTemplate.Domain.ValueObjects;

namespace NetCoreTemplate.Persistence.EF.Models;

public class ReportReadModel : AuditableEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ReportPriority ReportPriority { get; set; }
    public ReportType ReportType { get; set; }
    public Localization Localization { get; set; }
    public Address Address { get; set; }

    public User User { get; set; }
    public long UserId { get; set; }
}