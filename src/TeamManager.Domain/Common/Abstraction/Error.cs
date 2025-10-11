namespace TeamManager.Domain.Common.Abstraction;

public sealed record Error(string Code, int StatusCode, string? Description = null)
{
    public static readonly Error None = new(string.Empty, 200);
}