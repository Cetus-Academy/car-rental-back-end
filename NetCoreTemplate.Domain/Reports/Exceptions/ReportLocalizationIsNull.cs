using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Reports.Exceptions;

public class ReportLocalizationIsNull : NetCoreTemplateException
{
    public ReportLocalizationIsNull() : base("Localization is empty")
    {
    }
}