using System.ComponentModel.DataAnnotations;
using TeamManager.Application.Abstractions.Providers;

namespace TeamManager.Infrastructure.Configurations;

public class StrapiSettings : IStrapiSettings
{
    [Required] public string StrapiApiUrl { get; set; } = string.Empty;
    [Required] public string StrapiClientName { get; set; } = string.Empty;
}