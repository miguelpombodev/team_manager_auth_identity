using System.ComponentModel.DataAnnotations;
using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Configurations;

public class StrapiSettings : IStrapiSettings
{
    [Required] public string StrapiApiUrl { get; set; } = string.Empty;
    [Required] public string StrapiClientName { get; set; } = string.Empty;
}