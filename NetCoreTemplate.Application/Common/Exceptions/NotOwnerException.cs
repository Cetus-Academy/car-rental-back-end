using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class NotOwnerException : NetCoreTemplateException
{
    public NotOwnerException() : base("Only owner can modify this report.")
    {
    }
}