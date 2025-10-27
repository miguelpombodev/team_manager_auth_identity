using System.Text.Json;
using TeamManager.Domain.Providers.Communication;

namespace TeamManager.Domain.Members.Entities;

public class MemberCreatedEmailNotification : ServiceBusNotification
{
    public MemberCreatedEmailNotification(string to, string emailTemplate) : base(
        "sub-email-sender",
        "sub-email-sender-exchange",
        "sub-email",
        JsonSerializer.Serialize(new
        {
            to = to,
            subject = "Welcome To TeamManager!",
            body = emailTemplate
        }))
    {
    }
}