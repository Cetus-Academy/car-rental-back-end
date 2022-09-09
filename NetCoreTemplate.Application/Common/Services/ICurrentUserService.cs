using System.Security.Claims;

namespace NetCoreTemplate.Application.Common.Services;

public interface ICurrentUserService
{
    string Email { get;  }
    long UserId { get;  }
    public ClaimsPrincipal User { get; }
}