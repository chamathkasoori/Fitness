using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanDiscountClub : BaseEntity
{
    public int SubscriptionPlanDiscountId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;

    public int ClubId { get; set; }
    [JsonIgnore]
    public virtual Club Club { get; set; } = null!;
}
