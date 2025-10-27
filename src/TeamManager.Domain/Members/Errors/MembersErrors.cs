using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Members.Errors;

public static class MembersErrors
{
    public static readonly Error MemberAlreadyRegistered = new Error("Member.MemberAlreadyRegistered", 409, "Member already registered");
    public static readonly Error MemberNullableTokenVerification = new Error("Member.MemberNullableTokenVerification", 404, "Member token verification does not exist");
}