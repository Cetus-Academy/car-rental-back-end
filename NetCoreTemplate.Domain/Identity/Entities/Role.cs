using Microsoft.AspNetCore.Identity;

namespace NetCoreTemplate.Domain.Identity.Entities;

public class Role : IdentityRole<long>
{
    public Role()
    {
        
    }
    public Role(string roleName)
        : base(roleName)
    {
    }


    public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}