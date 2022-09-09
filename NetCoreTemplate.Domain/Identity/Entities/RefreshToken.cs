﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetCoreTemplate.Domain.Identity.Entities;
public class RefreshToken
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Revoked { get; set; }
    public string ReplacedByToken { get; set; }
    public string ReasonRevoked { get; set; }
    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsRevoked && !IsExpired;
}