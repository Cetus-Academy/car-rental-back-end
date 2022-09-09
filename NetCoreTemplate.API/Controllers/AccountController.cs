using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.API.Attributes;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Application.Common.Identity;
using NetCoreTemplate.Domain.Identity.Entities;
using NetCoreTemplate.Domain.Identity.Static;

namespace NetCoreTemplate.API.Controllers;

[Route("account")]
[ApiAuthorize]
public class AccountController : BaseController
{
    private readonly IIdentityService _identityService;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public AccountController(IIdentityService identityService, RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _identityService = identityService;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<JsonWebToken>> SignIn([FromBody] SignInDto dto)
    {
        var jwt = await _identityService.SignInAsync(dto);
        SetTokenCookie(jwt.RefreshToken);
        return Ok(jwt);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto dto)
    {
        await _identityService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpGet("me")]
    public async Task<ActionResult<AccountDto>> GetAccountDetails()
    {
        var account = await _identityService.GetAsync();
        return Ok(account);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var token = Request.Cookies["refreshToken"];
        var response = await _identityService.RefreshToken(token);
        SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RevokeTokenDto dto)
    {
        var token = dto.Token ?? Request.Cookies["refreshToken"]; // możliwe wyrzucenie błędu

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" }); // można to ładniej ogarnąć

        await _identityService.RevokeToken(token);
        return Ok();
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailDto dto)
    {
        await _identityService.ConfirmEmailAsync(dto);
        return Ok();
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }


    [HttpGet("create-roles")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAdmin()
    {
        List<string> roles = new List<string>()
        {
            UserRoles.Admin,
            UserRoles.User,
            UserRoles.Dev,
        };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var newRole = new Role(role);
                var res = await _roleManager
                    .CreateAsync(
                        newRole);

                if (!res.Succeeded)
                {
                    return BadRequest("Problem with creating roles");
                }
            }
        }

        var admin = new User()
        {
            UserName = "admin@cetuspro.com",
            Email = "admin@cetuspro.com",
            EmailConfirmed = true
        };

        IdentityResult result;

        if (await _userManager.Users.AnyAsync(a => a.Email == admin.Email))
        {
            var user = await _userManager.FindByEmailAsync(admin.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if(!userClaims.Any(a => a.Type == "dev" && a.Value == "true"))
                await _userManager.AddClaimAsync(user, new Claim("dev", "true"));
            
            return Ok();

        }

        if (!await _userManager.Users.AnyAsync(a => a.Email == admin.Email))
        {
            result = await _userManager.CreateAsync(admin, "!23Haslo");
        }
        else
        {
            return BadRequest("This user already exists");
        }

        if (result.Succeeded)
        {
            result = await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
            result = await _userManager.AddToRoleAsync(admin, UserRoles.Dev);
            result = await _userManager.AddToRoleAsync(admin, UserRoles.User);

            await _userManager.AddClaimAsync(admin, new Claim("email", admin.Email));
            await _userManager.AddClaimAsync(admin, new Claim("dev", "true"));
        }
        else
        {
            return BadRequest("There is a problem with adding roles to user");
        }

        return Ok("User was successfully created");
    }
    
   
}