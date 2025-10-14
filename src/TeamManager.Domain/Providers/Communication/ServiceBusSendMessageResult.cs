namespace RoyalRent.Domain.Common.Entities;

public class ServiceBusSendMessageResult
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}
