using NetCoreTemplate.Application.Common.DTO;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Domain.Reports.Enums;

namespace NetCoreTemplate.Application.Queries.GetReportList;

public record GetReportListQueryVm
{
    public long Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public ReportPriority ReportPriority { get; init; }
    public DateTimeOffset ReportDate { get; init; }
    public UserDto User { get; init; }
    public ReportType ReportType { get; init; }
    public LocalizationDto Localization { get; init; }
    public AddressDto Address { get; init; }

}