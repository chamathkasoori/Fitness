using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class AccessRuleItemTiming : BaseEntity
{
    public int AccessRuleItemId { get; set; }
    public virtual AccessRuleItem AccessRuleItem { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}
