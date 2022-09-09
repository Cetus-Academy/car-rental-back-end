namespace NetCoreTemplate.Application.Common.DTO.Identity;

public record UserDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Roles { get; set; } = new();
}