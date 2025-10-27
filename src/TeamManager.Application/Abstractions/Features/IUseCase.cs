namespace TeamManager.Application.Abstractions.Features;

public interface IUseCase<TResponse>
{
    Task<TResponse> ExecuteAsync();
}

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}

