using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;

namespace TeamManager.Application.Features.Member;

public class ConfirmMemberEmailUseCase : IUseCase<Guid, Result<bool>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IEmailTokenRepository _emailTokenRepository;

    public ConfirmMemberEmailUseCase(IEmailTokenRepository emailTokenRepository, IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
        _emailTokenRepository = emailTokenRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(Guid tokenId)
    {
        var tokenResult = await _emailTokenRepository.RetrieveByIdAsync(tokenId);

        if (tokenResult is null || tokenResult.ExpiresOnUtc < DateTime.UtcNow || tokenResult.User.EmailConfirmed)
            return Result<bool>.Failure(MembersErrors.MemberNullableTokenVerification);

        tokenResult.User.EmailConfirmed = true;

        await _memberRepository.UpdateEntityAsync(tokenResult.User);
        _emailTokenRepository.Remove(tokenResult);
        return Result<bool>.Success(true);
    }
}