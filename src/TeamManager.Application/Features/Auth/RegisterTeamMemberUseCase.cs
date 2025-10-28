using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Persistence;

namespace TeamManager.Application.Features.Auth;

public class RegisterTeamMemberUseCase : IUseCase<RegisterTeamMember, Result<EmailVerificationToken>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<RegisterTeamMemberUseCase> _logger;
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterTeamMemberUseCase(
        UserManager<ApplicationAuthUser> userManager,
        IMemberRepository memberRepository,
        ILogger<RegisterTeamMemberUseCase> logger,
        IUnitOfWork unitOfWork
    )
    {
        _userManager = userManager;
        _memberRepository = memberRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmailVerificationToken>> ExecuteAsync(RegisterTeamMember request)
    {
        var tryToGetUser = await _userManager.FindByEmailAsync(request.Email);

        if (tryToGetUser is not null)
        {
            _logger.LogCritical(
                "Code: {Code} - Description: {Description}",
                MembersErrors.MemberAlreadyRegistered.Code,
                MembersErrors.MemberAlreadyRegistered.Description
            );

            return Result<EmailVerificationToken>.Failure(MembersErrors.MemberAlreadyRegistered);
        }

        var user = ApplicationAuthUser.Build(request.Email, request.FullName);

        var userComplements = UserComplements.Build(request.FullName, user.Id);

        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                await transaction.RollbackAsync();

                var errors = string.Join(",", identityResult.Errors.Select(x => x.Description));
                return Result<EmailVerificationToken>.Failure(new Error(
                    "MemberRepositoryError",
                    Description: $"Failed to create a new member: {errors}"));
            }

            await _memberRepository.CreateUserComplements(userComplements);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("New user has been registered. User email: {UserEmail}", request.Email);

            var verificationToken = EmailVerificationToken.Build(user.Id);

            return Result<EmailVerificationToken>.Success(verificationToken);
        }
        catch (DbUpdateException e)
        {
            await transaction.RollbackAsync();
            return Result<EmailVerificationToken>.Failure(new Error(
                "Database.Error",
                Description: $"Falha ao salvar dados: {e.InnerException?.Message ?? e.Message}"));
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Result<EmailVerificationToken>.Failure(new Error(
                "UseCase.Error",
                Description: $"Erro inesperado: {e.Message}"));
        }
    }
}