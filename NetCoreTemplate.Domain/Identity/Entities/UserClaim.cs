using Microsoft.AspNetCore.Identity;

namespace NetCoreTemplate.Domain.Identity.Entities;

public class UserClaim : IdentityUserClaim<long>
{
    public User User { get; set; }
}