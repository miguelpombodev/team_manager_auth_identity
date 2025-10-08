namespace TeamManager.Domain.Entities;

public class UserComplements
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
}