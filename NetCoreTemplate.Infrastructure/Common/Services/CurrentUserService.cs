using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NetCoreTemplate.Application.Common.Services;

namespace NetCoreTemplate.Infrastructure.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _userId;
    

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public long UserId => Convert.ToInt64(_userId);
    public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public ClaimsPrincipal User => _httpContextAccessor?.HttpContext?.User;

}