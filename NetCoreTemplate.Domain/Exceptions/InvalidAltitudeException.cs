using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Exceptions;

public class InvalidAltitudeException : NetCoreTemplateException
{
    public InvalidAltitudeException() : base("Provide valid altitude.")
    {
    }
}