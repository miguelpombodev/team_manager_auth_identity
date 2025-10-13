namespace TeamManager.Domain.Providers.Cache;

public interface IDistribuitedCacheProvider
{
    T? GetData<T>(string key);
    void SetData<T>(string key, T data);
}