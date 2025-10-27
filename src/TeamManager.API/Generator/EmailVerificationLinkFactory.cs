using TeamManager.API.Endpoints;
using TeamManager.Domain.Members.Entities;

namespace TeamManager.API.Generator;

internal sealed class EmailVerificationLinkFactory(
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator,
    ILogger<EmailVerificationLinkFactory> _logger)
{
    public string Create(EmailVerificationToken emailVerificationToken)
    {
        var verificationLink = linkGenerator.GetUriByName(
            httpContextAccessor.HttpContext!,
            AuthEndpoints.VerifyEmailEndpointName,
            new { token = emailVerificationToken.Id });

        if (string.IsNullOrEmpty(verificationLink))
        {
            var errorLogMessageProperties = new { emailVerificationToken.Id, emailVerificationToken.UserId };

            _logger.LogWarning("Verification Link was created as null or empty - {@ErrorLogMessageProperties}", errorLogMessageProperties);
            throw new ArgumentException("Could not create email verification link");
        }

        return verificationLink;
    }
}