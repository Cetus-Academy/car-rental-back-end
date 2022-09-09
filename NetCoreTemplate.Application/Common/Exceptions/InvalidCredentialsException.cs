using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class InvalidCredentialsException : NetCoreTemplateException
{
    public InvalidCredentialsException() : base("Provided credentials are invalid.")
    {
    }
}