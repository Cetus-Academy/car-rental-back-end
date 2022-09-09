namespace NetCoreTemplate.Application.Common.DTO.Identity;

public record RevokeTokenDto
{
    public string Token { get; init; }
}