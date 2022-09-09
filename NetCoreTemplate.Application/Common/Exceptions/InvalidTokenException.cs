using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class InvalidTokenException : NetCoreTemplateException
{
    public InvalidTokenException() : base("Provided token is not valid.")
    {
    }
}