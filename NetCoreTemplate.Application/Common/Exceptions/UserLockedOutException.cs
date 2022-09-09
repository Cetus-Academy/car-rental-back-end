using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;

public class UserLockedOutException : NetCoreTemplateException
{
    public long UserId { get; }
    public DateTimeOffset? LockoutEnd { get;}
    public string ReasonWhy { get; }

    public UserLockedOutException(long userId, DateTimeOffset? lockoutEnd, string reasonWhy) : base("Your account is locked")
    {
        UserId = userId;
        LockoutEnd = lockoutEnd;
        ReasonWhy = reasonWhy;
    }
}