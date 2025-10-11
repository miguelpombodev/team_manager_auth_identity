using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Members.Errors;

public static class MembersErrors
{
    public static readonly Error MemberAlreadyRegistered = new Error("Member.MemberAlreadyRegistered", 409, "Member already registered");
}