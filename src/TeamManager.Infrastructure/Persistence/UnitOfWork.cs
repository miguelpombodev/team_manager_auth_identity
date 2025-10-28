using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TeamManager.Domain.Providers.Persistence;

namespace TeamManager.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        try
        {
            return await _context.Database.BeginTransactionAsync(ct);
        }
        catch (Exception e) 
        {
            _logger.LogCritical(
                e,
                "[CRITICAL] Falha ao INICIAR a transação do banco de dados - Mensagem: {ErrorMessage} Trace: {ErrorTrace}",
                e.Message, e.StackTrace);
            throw;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            return await _context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogCritical(
                e,
                "[CRITICAL] A concurrency violation is encountered when trying to saving any data in context - Message: {ErrorMessage} Trace: {ErrorTrace}",
                e.Message, e.StackTrace);

            throw;
        }
        catch (DbUpdateException e)
        {
            _logger.LogCritical(
                e,
                "[CRITICAL] There was an error when trying to saving any data in context - Message: {ErrorMessage} Trace: {ErrorTrace}",
                e.Message, e.StackTrace);

            throw;
        }
    }
}