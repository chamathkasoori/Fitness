namespace Fitness.Core.Entities;

public class PasswordHistory
{
    public int PasswordHistoryId { get; set; }

    public int LoginId { get; set; }

    public string? PasswordSalt { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime PostingDate { get; set; }

    public virtual Login Login { get; set; } = null!;
}