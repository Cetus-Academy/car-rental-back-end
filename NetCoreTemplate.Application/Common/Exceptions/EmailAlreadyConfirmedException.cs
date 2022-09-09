using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class EmailAlreadyConfirmedException : NetCoreTemplateException
{
    public EmailAlreadyConfirmedException() : base("Email is already confirmed.")
    {
    }
}