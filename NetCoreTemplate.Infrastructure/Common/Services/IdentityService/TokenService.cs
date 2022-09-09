using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Application.Common.Identity;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Infrastructure.Configurations.Identity;

namespace NetCoreTemplate.Infrastructure.Common.Services.IdentityService;
public class TokenService : ITokenService
{
    private static readonly Dictionary<string, string> EmptyClaims = new();
    private static readonly ICollection<string> EmptyRoles = new List<string>();
    private readonly IDateService _dateService;
    private readonly AuthConfig _authConfig;
    private readonly SigningCredentials _signingCredentials;
    private readonly string _issuer;

    public TokenService(IDateService dateService, AuthConfig authConfig)
    {
        _dateService = dateService;
        _authConfig = authConfig;
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JwtKey)),
                SecurityAlgorithms.HmacSha256);
        _issuer = authConfig.JwtIssuer;
    }

    public JsonWebToken GenerateAccessToken(long userId, ICollection<string> roles = null ,
        ICollection<Claim> claims = null)
    {
        
        var now = _dateService.CurrentDate();

        // Create List with JwtRegisteredClaims which are not mandatory but they are helpful to keep our secure on higher level
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };

        // Adding custom claims if there are any
        if (roles?.Any() is true) // or check if it is empty - Every collection in Model is initialized 
        {
            foreach (var role in roles)
            {
                jwtClaims.Add(new Claim("role", role));
            }
        }
        
        // Adding custom claims if there are any
        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var claim in claims)
            {
                {
                    customClaims.Add( new Claim(claim.Type, claim.Value));
                }
            }

            jwtClaims.AddRange(customClaims);
        }

        // Adding TimeSpan after which the Token expires.
        var expires = now.Add(_authConfig.Expires);

        
        // Creating Token
        var jwt = new JwtSecurityToken(
            _issuer,
            _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials);

        
        // Writing Token
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        // Mapping token to more user friendly DTO
        return new JsonWebToken()
        {
            AccessToken = token,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId = userId,
            Roles = roles ?? EmptyRoles,
            Claims = claims?.ToDictionary(a => a.Type, a=> a.Value) ?? EmptyClaims
        };
    }

    public RefreshToken GenerateRefreshToken()
    {
        // generate token that is valid for 7 days
        var randomBytes =  RandomNumberGenerator.GetBytes(32);
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes) + Guid.NewGuid(),
            Expires = _dateService.CurrentDate().AddDays(7),
            Created = _dateService.CurrentDate()
        };

        return refreshToken;
    }

    public void RemoveOldRefreshTokens(User user)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_authConfig.RefreshTokenTTL) <= _dateService.CurrentDate());
    }

    public void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string reason)
    {
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.FirstOrDefault(a => a.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
                RevokeRefreshToken(childToken, reason);
            else
                RevokeDescendantRefreshTokens(childToken, user, reason);
        }
    }


    public void RevokeRefreshToken(RefreshToken token, string reason = null, string replacedByToken = null)
    {
        token.Revoked = _dateService.CurrentDate();
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }

    public RefreshToken RotateRefreshToken(RefreshToken refreshToken)
    {
        var newRefreshToken = GenerateRefreshToken();
        RevokeRefreshToken(refreshToken, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }
}