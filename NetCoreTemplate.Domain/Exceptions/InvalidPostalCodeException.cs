using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Exceptions;

public class InvalidPostalCodeException : NetCoreTemplateException
{
    public InvalidPostalCodeException() : base("Provided postal code is invalid.")
    {
    }
}