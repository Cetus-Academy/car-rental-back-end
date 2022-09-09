using System.Security.Claims;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Domain.Identity.Entities;

namespace NetCoreTemplate.Application.Common.Identity;
public interface ITokenService
{
    JsonWebToken GenerateAccessToken(long userId, ICollection<string> roles = null,
        ICollection<Claim> claims = null);

    RefreshToken GenerateRefreshToken();
    void RemoveOldRefreshTokens(User user);
    void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string reason);
    void RevokeRefreshToken(RefreshToken token, string reason = null, string replacedByToken = null);
    RefreshToken RotateRefreshToken(RefreshToken refreshToken);
}