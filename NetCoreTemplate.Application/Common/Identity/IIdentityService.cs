using NetCoreTemplate.Application.Common.DTO.Identity;

namespace NetCoreTemplate.Application.Common.Identity;

public interface IIdentityService
{
    Task<AccountDto> GetAsync();
    Task<JsonWebToken> SignInAsync(SignInDto dto);
    Task SignUpAsync(SignUpDto dto);
    Task<JsonWebToken> RefreshToken(string token);
    Task RevokeToken(string token);
    Task ConfirmEmailAsync(ConfirmEmailDto dto);
}