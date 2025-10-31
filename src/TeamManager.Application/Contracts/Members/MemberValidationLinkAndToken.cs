using TeamManager.Domain.Members.Entities;

namespace TeamManager.Application.Contracts.Members;

public sealed record MemberValidationLinkAndToken(string LinkValidation, EmailVerificationToken EmailVerificationToken);