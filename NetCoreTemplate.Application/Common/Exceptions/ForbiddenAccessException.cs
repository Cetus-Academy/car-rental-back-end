using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class ForbiddenAccessException : NetCoreTemplateException
{
    public ForbiddenAccessException() : base("You don't have access to these resources.")
    {
    }
}