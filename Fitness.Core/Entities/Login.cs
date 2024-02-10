namespace Fitness.Core.Entities;

public class Login
{
    public int LoginId { get; set; }

    public string? UserName { get; set; }

    public string? PasswordSalt { get; set; }

    public string? PasswordHash { get; set; }

    public int UserId { get; set; }

    public bool IsEmployee { get; set; }

    public virtual ICollection<PasswordHistory> PasswordHistories { get; set; } = new List<PasswordHistory>();

    public virtual User User { get; set; } = null!;
}