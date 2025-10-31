using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Enums;

namespace TeamManager.Infrastructure.Email;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    public IEmailBodyBuilder CreateBuilder(EmailTemplateType emailType)
    {
        switch (emailType)
        {
            case EmailTemplateType.WelcomeAndConfirmEmail:
                return new WelcomeEmailBuilder();
            
            default:
                throw new NotSupportedException($"Email type '{emailType}' is not supported for email factory.");
        }
    }
}