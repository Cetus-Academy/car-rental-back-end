using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class NotFoundException : NetCoreTemplateException
{
    public NotFoundException(string message) : base(message)
    {
    }
}