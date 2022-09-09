using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Persistence;
using NetCoreTemplate.Persistence.EF.Context;

namespace NetCoreTemplate.Infrastructure.Common.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly DbSet<User> _users;

    public UserService(UserManager<User> userManager, ReadDbContext dbContext)
    {
        _userManager = userManager;
        _users = dbContext.Users;
    }

    public async Task<List<string>> GetUserRolesAsync(long userId)
    {
        var roles = new List<string>();
        
        var user = await _users.FindAsync(userId);
        if (user is null)
        {
            return roles;
        }
        var userRoles = await _userManager.GetRolesAsync(user);

        roles = userRoles as List<string>;
        
        return roles;
    }
}