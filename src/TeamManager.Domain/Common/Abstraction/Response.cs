namespace TeamManager.Domain.Common.Abstraction;

public class Response
{
    public object? Data { get; set; } = null;

    public Response(object? data)
    {
        Data = data;
    }
}