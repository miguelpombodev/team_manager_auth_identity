using TeamManager.Domain.Members.Entities;

namespace TeamManager.Application.Abstractions.Requests;

public sealed record MemberValidationLinkAndToken(string LinkValidation, EmailVerificationToken EmailVerificationToken);