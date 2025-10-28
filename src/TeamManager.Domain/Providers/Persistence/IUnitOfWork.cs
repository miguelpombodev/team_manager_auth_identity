using Microsoft.EntityFrameworkCore.Storage;

namespace TeamManager.Domain.Providers.Persistence;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
    
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}