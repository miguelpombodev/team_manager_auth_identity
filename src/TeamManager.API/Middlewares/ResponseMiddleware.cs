using System.Text;
using System.Text.Json;
using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.API.Middlewares;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonOptions;


    public ResponseMiddleware(RequestDelegate next)
    {
        _next = next;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Response.StatusCode == 201)
        {
            await _next(context);
        }
        var originalBodyStream = context.Response.Body;

        try
        {
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            if (!context.Response.HasStarted)
            {
                await ProcessResponse(context, responseBody, originalBodyStream);
            }
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task ProcessResponse(HttpContext context, MemoryStream responseBody, Stream originalBodyStream)
    {
        responseBody.Seek(0, SeekOrigin.Begin);

        var statusCode = context.Response.StatusCode;
        var isSuccess = statusCode >= 200 && statusCode < 300;

        var responseText = await new StreamReader(responseBody).ReadToEndAsync();

        var standardResponse = CreateStandardResponse(isSuccess, responseText);

        var jsonResponse = JsonSerializer.Serialize(standardResponse, _jsonOptions);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.ContentLength = responseBytes.Length;

        await originalBodyStream.WriteAsync(responseBytes);
    }

    private Response CreateStandardResponse(bool isSuccess, string originalContent)
    {
        if (isSuccess)
        {
            var data = TryParseJson(originalContent);

            return new Response(data);
        }

        var errorData = TryParseJson(originalContent);

        return new Response(errorData ?? originalContent);
    }

    private object? TryParseJson(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return null;
        try
        {
            return JsonSerializer.Deserialize<object>(content);
        }
        catch
        {
            return content;
        }
    }
}