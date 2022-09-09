namespace NetCoreTemplate.Application.Common.Services;
public interface IDateService
{
    DateTime CurrentDate();
    DateTimeOffset CurrentOffsetDate();
}