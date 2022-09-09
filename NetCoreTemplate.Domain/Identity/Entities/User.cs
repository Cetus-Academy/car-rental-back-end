using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace NetCoreTemplate.Domain.Identity.Entities;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; } = new();

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

}