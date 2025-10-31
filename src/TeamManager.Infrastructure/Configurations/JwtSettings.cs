using System.ComponentModel.DataAnnotations;
using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Configurations;

public class JwtSettings : IJwtSettings
{
    [Required] public string Issuer { get; set; } = string.Empty;
    [Required] public string Audience { get; set; } = string.Empty;
    [Required] public string Secret { get; set; } = string.Empty;
    [Range(1, 1440)] public int ExpirationInMinutes { get; set; }
}