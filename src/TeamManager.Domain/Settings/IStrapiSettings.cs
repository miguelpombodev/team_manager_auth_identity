namespace TeamManager.Domain.Settings;

public interface IStrapiSettings
{
    string StrapiApiUrl { get; }
    string StrapiClientName { get; }
}