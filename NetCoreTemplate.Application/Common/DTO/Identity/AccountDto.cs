namespace NetCoreTemplate.Application.Common.DTO.Identity;

public record AccountDto
{
    public long Id { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public ICollection<string> Roles { get; init; } = new List<string>();
}