using TeamManager.Domain.Members.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface IEmailBodyBuilder
{
    string BuildEmailBody(EmailTemplate emailTemplate, Dictionary<string, string> data);
}