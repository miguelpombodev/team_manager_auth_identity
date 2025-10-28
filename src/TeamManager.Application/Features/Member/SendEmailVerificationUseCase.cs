using Microsoft.Extensions.Logging;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Providers.Persistence;

namespace TeamManager.Application.Features.Member;

public class SendEmailVerificationUseCase : IUseCase<MemberValidationLinkAndToken, Result<bool>>
{
    private const string EmailBaseTemplate =
        "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Welcome to Team Manager</title>\n    <style>\n        * {\n            margin: 0;\n            padding: 0;\n            box-sizing: border-box;\n        }\n        \n        body {\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;\n            background-color: #F9F7F7;\n            padding: 20px;\n        }\n        \n        .email-container {\n            max-width: 600px;\n            margin: 0 auto;\n            background-color: #FFFFFF;\n            border-radius: 16px;\n            overflow: hidden;\n            box-shadow: 0 10px 40px rgba(17, 45, 78, 0.1);\n        }\n        \n        .header {\n            background: linear-gradient(135deg, #112D4E 0%, #3F72AF 100%);\n            padding: 40px 30px;\n            text-align: center;\n            position: relative;\n            overflow: hidden;\n        }\n        \n        .header::before {\n            content: '';\n            position: absolute;\n            top: -50%;\n            right: -20%;\n            width: 300px;\n            height: 300px;\n            background: rgba(219, 226, 239, 0.1);\n            border-radius: 50%;\n        }\n        \n        .header::after {\n            content: '';\n            position: absolute;\n            bottom: -30%;\n            left: -10%;\n            width: 200px;\n            height: 200px;\n            background: rgba(219, 226, 239, 0.1);\n            border-radius: 50%;\n        }\n        \n        .logo {\n            font-size: 28px;\n            font-weight: 700;\n            color: #F9F7F7;\n            margin-bottom: 10px;\n            position: relative;\n            z-index: 1;\n        }\n        \n        .welcome-title {\n            font-size: 32px;\n            font-weight: 700;\n            color: #F9F7F7;\n            margin-top: 20px;\n            position: relative;\n            z-index: 1;\n        }\n        \n        .content {\n            padding: 50px 40px;\n            text-align: center;\n        }\n        \n        .illustration {\n            margin: 30px 0;\n            max-width: 100%;\n        }\n        \n        .illustration img {\n            max-width: 450px;\n            width: 100%;\n            height: auto;\n            display: block;\n            margin: 0 auto;\n        }\n        \n        .message {\n            font-size: 18px;\n            color: #112D4E;\n            line-height: 1.8;\n            margin-bottom: 20px;\n        }\n        \n        .highlight {\n            color: #3F72AF;\n            font-weight: 600;\n        }\n        \n        .info-box {\n            background-color: #DBE2EF;\n            border-left: 4px solid #3F72AF;\n            padding: 20px;\n            margin: 30px 0;\n            text-align: left;\n            border-radius: 8px;\n        }\n        \n        .info-box h3 {\n            color: #112D4E;\n            font-size: 18px;\n            margin-bottom: 10px;\n        }\n        \n        .info-box p {\n            color: #112D4E;\n            font-size: 15px;\n            line-height: 1.6;\n        }\n        \n        .cta-button {\n            display: inline-block;\n            background: linear-gradient(135deg, #3F72AF 0%, #112D4E 100%);\n            color: #F9F7F7;\n            padding: 16px 40px;\n            text-decoration: none;\n            border-radius: 30px;\n            font-weight: 600;\n            font-size: 16px;\n            margin-top: 20px;\n            transition: transform 0.3s ease, box-shadow 0.3s ease;\n            box-shadow: 0 4px 15px rgba(63, 114, 175, 0.3);\n        }\n        \n        .cta-button:hover {\n            transform: translateY(-2px);\n            box-shadow: 0 6px 20px rgba(63, 114, 175, 0.4);\n        }\n        \n        .footer {\n            background-color: #F9F7F7;\n            padding: 30px;\n            text-align: center;\n            border-top: 1px solid #DBE2EF;\n        }\n        \n        .footer p {\n            color: #112D4E;\n            font-size: 14px;\n            line-height: 1.6;\n        }\n        \n        .social-links {\n            margin-top: 20px;\n        }\n        \n        .social-links a {\n            display: inline-block;\n            margin: 0 10px;\n            color: #3F72AF;\n            text-decoration: none;\n            font-size: 14px;\n        }\n\n        @media only screen and (max-width: 600px) {\n            .content {\n                padding: 30px 20px;\n            }\n            \n            .welcome-title {\n                font-size: 26px;\n            }\n            \n            .message {\n                font-size: 16px;\n            }\n        }\n    </style>\n</head>\n<body>\n    <div class=\"email-container\">\n        <div class=\"header\">\n            <div class=\"logo\">Team Manager</div>\n            <div class=\"welcome-title\">Welcome, {username}! 🎉</div>\n        </div>\n        \n        <div class=\"content\">\n            <p class=\"message\">\n                We're thrilled to have you join our platform! Your account has been successfully created and you're all set to start your journey with us.\n            </p>\n            \n            <div class=\"illustration\">\n                <img src=\"https://images.blush.design/516dbfee93b4fd4a4c5a2601950a6452?w=920&auto=compress&cs=srgb\" alt=\"Team collaboration illustration\">\n            </div>\n            \n            <div class=\"info-box\">\n                <h3>📋 You're not assigned to a team yet</h3>\n                <p>\n                    Don't worry! Our team administrators are working on getting you set up. \n                    You'll be assigned to a team soon and will receive a notification once that happens.\n                </p>\n            </div>\n            \n            <p class=\"message\">\n                In the meantime, feel free to explore the platform and familiarize yourself with the features. \n                Once you're assigned to a team, you'll be able to <span class=\"highlight\">collaborate</span>, \n                <span class=\"highlight\">manage tasks</span>, and <span class=\"highlight\">track progress</span> \n                with your teammates!\n            </p>\n            \n            <p class=\"message\">But first we need you to confirm your email by clicking on the button below</p>\n            \n            <a href=\"#\" class=\"cta-button\">Confirm Email</a>\n        </div>\n        \n        <div class=\"footer\">\n            <p>\n                If you have any questions or need assistance, feel free to reach out to our support team.\n            </p>\n            <p style=\"margin-top: 10px;\">\n                <strong>Team Manager</strong> - Empowering teams to achieve more\n            </p>\n            <div class=\"social-links\">\n                <a href=\"#\">Help Center</a> • \n                <a href=\"#\">Contact Support</a> • \n                <a href=\"#\">Documentation</a>\n            </div>\n        </div>\n    </div>\n</body>\n</html>\n";
    
    private readonly IEmailTokenRepository _emailTokenRepository;
    private readonly ILogger<SendEmailVerificationUseCase> _logger;
    private readonly IServiceBusProvider _serviceBusProvider;
    private readonly IUnitOfWork _unitOfWork;


    public SendEmailVerificationUseCase(
        IEmailTokenRepository emailTokenRepository,
        ILogger<SendEmailVerificationUseCase> logger,
        IServiceBusProvider serviceBusProvider,
        IUnitOfWork unitOfWork)
    {
        _emailTokenRepository = emailTokenRepository;
        _logger = logger;
        _serviceBusProvider = serviceBusProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> ExecuteAsync(MemberValidationLinkAndToken request)
    {
        var result = await _emailTokenRepository.AddAsync(request.EmailVerificationToken);

        await _unitOfWork.SaveChangesAsync();
        
        // Create factory for member notification
        var templateWithLinkValidation = EmailBaseTemplate.Replace("{username}", result.User.UserComplements.FullName).Replace(
            "<a href=\"#\" class=\"cta-button\">",
            $"<a href=\"{request.LinkValidation}\" class=\"cta-button\">");
        
        var memberRegisteredEmailNotification =
            new MemberCreatedEmailNotification(result.User.Email!, templateWithLinkValidation);

        await _serviceBusProvider.SendMessage(memberRegisteredEmailNotification);

        return Result<bool>.Success(true);
    }
}