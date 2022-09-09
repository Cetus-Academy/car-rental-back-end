using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class EmailNotConfirmedException : NetCoreTemplateException
{
    public EmailNotConfirmedException() : base("Email is not confirmed.")
    {
    }
}