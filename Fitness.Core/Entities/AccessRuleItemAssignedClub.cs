using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class AccessRuleItemAssignedClub : BaseEntity
{
    public int AccessRuleItemId { get; set; }
    public virtual AccessRuleItem AccessRuleItem { get; set; } = null!;

    public int ClubId { get; set; }
    public virtual Club Club { get; set; } = null!;
}
