using NetCoreTemplate.Application.Common.Services;

namespace NetCoreTemplate.Infrastructure.Common.Services;
public class DateService : IDateService
{
    public DateTime CurrentDate() => DateTime.UtcNow;
    public DateTimeOffset CurrentOffsetDate() => DateTimeOffset.UtcNow;
}