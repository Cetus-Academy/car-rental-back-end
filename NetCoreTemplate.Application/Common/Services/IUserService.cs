namespace NetCoreTemplate.Application.Common.Services;

public interface IUserService
{
    Task<List<string>> GetUserRolesAsync(long userId);
}