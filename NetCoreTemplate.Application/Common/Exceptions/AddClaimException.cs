using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class AddClaimException : NetCoreTemplateException
{
    public AddClaimException() : base("There was an error during adding claim to user.")
    {
    }
}