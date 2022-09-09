using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class IdentityErrorException : NetCoreTemplateException
{
    public Dictionary<string, string> Errors { get; }
    public IdentityErrorException(Dictionary<string,string> errors) : base("Identity Error occured")
    {
        Errors = errors;
    }
}