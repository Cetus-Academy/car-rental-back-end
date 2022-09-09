namespace NetCoreTemplate.Application.Common.DTO.Identity;

public record ConfirmEmailDto(long UserId, string Token);