using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Enums;

namespace TeamManager.Domain.Members.Abstractions;

public interface IEmailTemplateFactory
{
    IEmailBodyBuilder CreateBuilder(EmailTemplateType emailType);
}