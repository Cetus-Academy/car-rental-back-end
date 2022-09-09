using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class NotActiveTokenException : NetCoreTemplateException
{
    public NotActiveTokenException() : base("Provided token is not active.")
    {
    }
}