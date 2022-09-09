using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class AddToRoleException : NetCoreTemplateException
{
    public AddToRoleException() : base($"Error occured during adding roles to user.")
    {
    }
}