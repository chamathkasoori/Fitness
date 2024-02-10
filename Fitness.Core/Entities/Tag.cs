using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public TagType Type { get; set; }

    public ICollection<SubscriptionPlanTag> SubscriptionPlanTags { get; set; } = new List<SubscriptionPlanTag>();
}