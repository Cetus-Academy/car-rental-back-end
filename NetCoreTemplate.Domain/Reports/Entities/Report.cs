using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Domain.Reports.Enums;
using NetCoreTemplate.Domain.Reports.Exceptions;
using NetCoreTemplate.Domain.ValueObjects;

namespace NetCoreTemplate.Domain.Reports.Entities;

public class Report
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get;  private set; }
    public ReportPriority ReportPriority { get; private set; }
    public ReportType ReportType { get; private set; }
    public Localization Localization { get; private set; }
    public Address Address { get; private set; }

    public User User { get; private set; }
    public long UserId { get;  private set; }

    private Report()
    {
    }

    public Report(string title, Localization localization, long userId)
    {
        SetTitle(title);
        SetLocalization(localization);
        UserId = userId;
    }

    public void SetTitle(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new TitleLocalizationIsNull();
        Title = title;
    }
    
    public void SetLocalization(Localization localization)
    {
        Localization = localization ?? throw new ReportLocalizationIsNull();
    }
    public void SetAddress(Address address)
    {
        Address = address;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }
    public void SetReportType(ReportType reportType)
    {
        ReportType = reportType;
    }
}