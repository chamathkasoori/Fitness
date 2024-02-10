using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class AccessRuleItem : BaseEntity
{
    public int AccessRuleId { get; set; }
    public virtual AccessRule AccessRule { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public bool AddToAllClubs { get; set; } = false;

    public bool IsActiveForNewClubs { get; set; } = false;

    public virtual ICollection<AccessRuleItemAssignedClub> AccessRuleItemAssignedClubs { get; set; } = new List<AccessRuleItemAssignedClub>();

    public virtual ICollection<AccessRuleItemTiming> AccessRuleItemTimings { get; set; } = new List<AccessRuleItemTiming>();
}
