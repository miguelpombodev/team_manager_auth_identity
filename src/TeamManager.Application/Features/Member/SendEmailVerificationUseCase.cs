using Microsoft.Extensions.Logging;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Providers;
using TeamManager.Application.Abstractions.Requests;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Enums;
using TeamManager.Domain.Providers.Communication;
using TeamManager.Domain.Providers.Persistence;

namespace TeamManager.Application.Features.Member;

public class SendEmailVerificationUseCase : IUseCase<MemberValidationLinkAndToken, Result<bool>>
{
    private readonly IEmailTokenRepository _emailTokenRepository;
    private readonly ILogger<SendEmailVerificationUseCase> _logger;
    private readonly IServiceBusProvider _serviceBusProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpClientProvider _httpClientProvider;
    private readonly IEmailTemplateFactory _emailFactory;
    private readonly IStrapiSettings _strapiSettings;

    private readonly string _getEmailTemplatesUrl;

    public SendEmailVerificationUseCase(
        IEmailTokenRepository emailTokenRepository,
        ILogger<SendEmailVerificationUseCase> logger,
        IServiceBusProvider serviceBusProvider,
        IUnitOfWork unitOfWork,
        IHttpClientProvider httpClientProvider,
        IEmailTemplateFactory emailFactory,
        IStrapiSettings strapiSettings)
    {
        _emailTokenRepository = emailTokenRepository;
        _logger = logger;
        _serviceBusProvider = serviceBusProvider;
        _unitOfWork = unitOfWork;
        _httpClientProvider = httpClientProvider;
        _emailFactory = emailFactory;
        _strapiSettings = strapiSettings;
        
        _getEmailTemplatesUrl = $"{_strapiSettings.StrapiApiUrl}/email-templates?template_id=WELCOME_EMAIL";
    }

    public async Task<Result<bool>> ExecuteAsync(MemberValidationLinkAndToken request)
    {
        var result = await _emailTokenRepository.AddAsync(request.EmailVerificationToken);

        await _unitOfWork.SaveChangesAsync();

        var emailBaseTemplateResult =
            await _httpClientProvider.Get<List<EmailTemplate>>(_strapiSettings.StrapiClientName, _getEmailTemplatesUrl);

        if (emailBaseTemplateResult.IsFailure || emailBaseTemplateResult.Data is null)
        {
            return Result<bool>.Failure(emailBaseTemplateResult.Error);
        }

        var emailTemplate = emailBaseTemplateResult.Data.FirstOrDefault(email => email.TemplateId == "WELCOME_EMAIL");

        var emailBuilder = _emailFactory.CreateBuilder(EmailTemplateType.WelcomeAndConfirmEmail);
        
        var emailData = new Dictionary<string, string>
        {
            { "UserFullName", result.User.UserComplements.FullName },
            { "ValidationLink",  request.LinkValidation }
        };

        var templateWithLinkValidation =
            emailBuilder.BuildEmailBody(
                emailTemplate,
                emailData
            );

        var memberRegisteredEmailNotification =
            new MemberCreatedEmailNotification(result.User.Email!, templateWithLinkValidation);

        await _serviceBusProvider.SendMessage(memberRegisteredEmailNotification);

        return Result<bool>.Success(true);
    }
}