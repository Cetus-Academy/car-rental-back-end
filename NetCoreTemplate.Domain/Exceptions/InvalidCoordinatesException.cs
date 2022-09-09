using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Exceptions;

public class InvalidCoordinatesException : NetCoreTemplateException
{
    public InvalidCoordinatesException() : base("Provided coordinates are invalid.")
    {
        
    }
}