using System.Text.Json;
using Microsoft.Extensions.Logging;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Providers.Communication;

namespace TeamManager.Infrastructure.Providers.Communication;

public class HttpClientProvider : IHttpClientProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpClientProvider> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    
    public HttpClientProvider(IHttpClientFactory httpClientFactory, ILogger<HttpClientProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
    
    public async Task<Result<T>> Get<T>(string clientName, string url)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("There was an error trying to retrieve data from {ClientName} | URL - [GET] {Url}", clientName, url);
            return Result<T>.Failure(new Error("HttpClient.Error", Description: $"Something went wrong trying to retrieve data from {clientName}"));
        }

        var json = await response.Content.ReadAsStringAsync();

        var serializedResponse = JsonSerializer.Deserialize<T>(json, _jsonOptions);
        
        if (serializedResponse is null)
        {
            _logger.LogWarning("Response came from {ClientName} is nullable | URL - [GET] {Url}", clientName, url);
            return Result<T>.Failure(new Error("HttpClient.Error", Description: $"Something went wrong trying to retrieve data from {clientName}"));
        }

        return Result<T>.Success(serializedResponse);
    }
}