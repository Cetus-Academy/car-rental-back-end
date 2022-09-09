using System.Text.Json.Serialization;

namespace NetCoreTemplate.Application.Common.DTO.Identity;
public record JsonWebToken
{
    public string AccessToken { get; init; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
    public long Expires { get; init; }
    public long UserId { get; init; }
    public string Email { get; set; }
    public ICollection<string> Roles { get; init; } = new List<string>();
    public IDictionary<string, string> Claims { get; init; } = new Dictionary<string, string>();

}