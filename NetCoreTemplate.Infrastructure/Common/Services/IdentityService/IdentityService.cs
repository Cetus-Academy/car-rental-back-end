using System.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Application.Common.Exceptions;
using NetCoreTemplate.Application.Common.Identity;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Domain.Identity.Static;
using NetCoreTemplate.Persistence;
using NetCoreTemplate.Persistence.EF.Context;

namespace NetCoreTemplate.Infrastructure.Common.Services.IdentityService;

public class IdentityService : IIdentityService
{
    private readonly ReadDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public IdentityService(ReadDbContext db, UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService, ICurrentUserService currentUserService)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _currentUserService = currentUserService;
    }

    public async Task<AccountDto> GetAsync()
    {
        var currentUserId = _currentUserService.UserId;

        var user = await _userManager.FindByIdAsync(currentUserId.ToString()) ??
                   throw new NotFoundException($"User could not be found.");

        var roles = await _userManager.GetRolesAsync(user);

        return new AccountDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = roles,
        };
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email) ??
                   throw new NotFoundException($"User with provided email: '{dto.Email}' could not be found.");

        // if (!user.EmailConfirmed && await _userManager.CheckPasswordAsync(user, dto.Password))
        //     throw new EmailNotConfirmedException();
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

        if (result.IsLockedOut)
            throw new UserLockedOutException(user.Id, user.LockoutEnd, "Too many login failures.");
        
        if (!result.Succeeded)
            throw new InvalidCredentialsException();

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        var jwt = _tokenService.GenerateAccessToken(user.Id, roles, claims: claims);
        jwt.Email = dto.Email;

        var refresh = _tokenService.GenerateRefreshToken();
        jwt.RefreshToken = refresh.Token;

        user.RefreshTokens.Add(refresh);
        _tokenService.RemoveOldRefreshTokens(user);

        await _db.SaveChangesAsync();

        return jwt;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        bool isNotEmailUnique = await _db.Users.AnyAsync(a => a.Email == dto.Email);
        if (isNotEmailUnique)
        {
            throw new EmailNotUniqueException();
        }

        dto.FirstName = dto.FirstName.Trim();
        dto.LastName = dto.LastName.Trim();

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Email,
            Email = dto.Email,
        };


        await using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        var createUserResult = await _userManager.CreateAsync(user, dto.Password);

        if (!createUserResult.Succeeded)
            throw new CreateUserException(createUserResult.Errors);

        var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addRoleResult.Succeeded)
            throw new AddToRoleException();

        var addClaimResult = await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
        if (!addClaimResult.Succeeded)
            throw new AddClaimException();

        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        
    }

    public async Task<JsonWebToken> RefreshToken(string token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(a => a.RefreshTokens.Any(b => b.Token == token)) ??
                   throw new InvalidTokenException();
        var currentRefreshToken = user.RefreshTokens.Single(a => a.Token == token);

        if (currentRefreshToken.IsRevoked)
        {
            _tokenService.RevokeDescendantRefreshTokens(currentRefreshToken, user,
                $"Attempted reuse of revoked ancestor token: {token}");
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        if (!currentRefreshToken.IsActive)
            throw new NotActiveTokenException();

        var newRefreshToken = _tokenService.RotateRefreshToken(currentRefreshToken);
        user.RefreshTokens.Add(newRefreshToken);

        _tokenService.RemoveOldRefreshTokens(user);

        _db.Update(user); // not necessary - check
        await _db.SaveChangesAsync();

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        var jwt = _tokenService.GenerateAccessToken(user.Id, roles, claims);

        jwt.RefreshToken = newRefreshToken.Token;

        return jwt;
    }

    public async Task RevokeToken(string token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token)) ??
                   throw new InvalidTokenException();

        var currentRefreshToken = user.RefreshTokens.Single(a => a.Token == token);

        if (!currentRefreshToken.IsActive)
            throw new NotActiveTokenException();

        _tokenService.RevokeRefreshToken(currentRefreshToken, "Revoked without replacement");
        _db.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task ConfirmEmailAsync(ConfirmEmailDto dto)
    {
        var (userId, token) = dto;
        var user = await _userManager.FindByIdAsync(userId.ToString()) ??
                   throw new NotFoundException($"User with provided Id: '{userId}' could not be found.");

        if (user.EmailConfirmed)
            throw new EmailAlreadyConfirmedException();

        if (string.IsNullOrEmpty(token))
            throw new InvalidTokenException();


        token = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        
        
        var result = await _userManager.ConfirmEmailAsync(user, token);

       

        if (!result.Succeeded)
        {
            var errors = result.Errors.ToDictionary(a => a.Code, a => a.Description);
            throw new IdentityErrorException(errors);
        }
        
    }
}