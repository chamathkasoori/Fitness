using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanAddonClub : BaseEntity
{
    public int SubscriptionPlanAddonId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlanAddon SubscriptionPlanAddon { get; set; } = null!;

    public int ClubId { get; set; }
    [JsonIgnore]
    public virtual Club Club { get; set; } = null!;
}
