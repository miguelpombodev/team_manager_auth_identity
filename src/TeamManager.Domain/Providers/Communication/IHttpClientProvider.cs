using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Providers.Communication;

public interface IHttpClientProvider
{
    Task<Result<T>> Get<T>(string clientName, string url);
}