namespace TeamManager.Application.Abstractions.Providers;

public interface IStrapiSettings
{
    string StrapiApiUrl { get; }
    string StrapiClientName { get; }
}