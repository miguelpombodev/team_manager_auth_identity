namespace TeamManager.Domain.Members.Common.Abstractions;

public interface IRepository
{
    Task<T> CreateAsync<T>(T entity);
    Task<T?> RetrieveEntityByIdAsync<T>(Guid id);
    Task UpdateEntityAsync<T>(T entity);
    
}