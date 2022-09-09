using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class EmailNotUniqueException : NetCoreTemplateException
{
    public EmailNotUniqueException() : base("This email is already taken.")
    {
    }
}