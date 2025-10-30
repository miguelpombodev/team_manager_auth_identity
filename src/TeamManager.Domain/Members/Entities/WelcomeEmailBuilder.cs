using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Domain.Members.Entities;

public class WelcomeEmailBuilder : IEmailBodyBuilder
{
    public string BuildEmailBody(EmailTemplate emailTemplate, Dictionary<string, string> data)
    {
        
        if (!data.TryGetValue("UserFullName", out var userFullName))
        {
            throw new ArgumentException("O placeholder 'UserFullName' é obrigatório para o WelcomeEmailBuilder.");
        }
        
        if (!data.TryGetValue("ValidationLink", out var validationLink))
        {
            throw new ArgumentException("O placeholder 'ValidationLink' é obrigatório para o WelcomeEmailBuilder.");
        }

        var templateWithLinkValidation = emailTemplate!.BodyHtml.Replace("{username}", userFullName).Replace(
            "<a href=\"#\" class=\"cta-button\">",
            $"<a href=\"{validationLink}\" class=\"cta-button\">");

        return templateWithLinkValidation;
    }
    
}