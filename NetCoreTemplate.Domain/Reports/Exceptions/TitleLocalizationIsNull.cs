using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Reports.Exceptions;

public class TitleLocalizationIsNull : NetCoreTemplateException
{
    public TitleLocalizationIsNull() : base("Title is empty")
    {
    }
}