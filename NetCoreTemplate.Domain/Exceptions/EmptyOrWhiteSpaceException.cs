using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Domain.Exceptions;

public class EmptyOrWhiteSpaceException : NetCoreTemplateException
{
    public EmptyOrWhiteSpaceException(string message) : base(message)
    {
    }
}