using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Providers.Communication.Interfaces;

namespace TeamManager.Infrastructure.Providers.Communication;

public sealed class RabbitMqPersistentConnection : IDisposable, IRabbitMqConnection
{
    const ushort MaxOutstandingConfirms = 256;

    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private bool _disposed;
    private readonly object _lockObject = new object();

    private readonly CreateChannelOptions _channelOptions = new CreateChannelOptions(
        publisherConfirmationsEnabled: true,
        publisherConfirmationTrackingEnabled: true,
        outstandingPublisherConfirmationsRateLimiter: new ThrottlingRateLimiter(MaxOutstandingConfirms));

    public RabbitMqPersistentConnection()
    {
        _factory = new ConnectionFactory
        {
            HostName = AppSettings.RabbitMqHostName,
            Port = AppSettings.RabbitMqPort,
            UserName = AppSettings.RabbitMqUserName,
            Password = AppSettings.RabbitMqPassword
        };
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        await EnsureConnectionAsync();

        if (_connection?.IsOpen != true)
            throw new InvalidOperationException("RabbitMQ connection is not open");

        return await _connection.CreateChannelAsync(_channelOptions);
    }

    private async Task EnsureConnectionAsync()
    {
        if (_connection?.IsOpen == true)
            return;

        lock (_lockObject)
        {
            if (_connection?.IsOpen == true)
                return;

            _connection?.Dispose();
            _connection = null;
        }

        _connection = await _factory.CreateConnectionAsync();
    }

    public async Task StartConnectionAsync(CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync();
    }

    public async Task StopConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection != null)
        {
            await _connection.CloseAsync(cancellationToken);
            _connection = null;
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
