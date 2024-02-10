namespace Fitness.Core.Entities;

public class TermsAndCondition
{
    public int TermsId { get; set; }

    public int UserId { get; set; }

    public string? Terms { get; set; }

    public virtual User User { get; set; } = null!;
}