using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class SubscriptionPlanAssignedClub : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int ClubId { get; set; }
    [JsonIgnore]
    public virtual Club Club { get; set; } = null!;
}