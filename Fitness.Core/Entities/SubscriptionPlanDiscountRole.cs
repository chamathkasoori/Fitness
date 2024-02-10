using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanDiscountRole : BaseEntity
{
    public int SubscriptionPlanDiscountId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;

    public int RoleId { get; set; }
    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;
}
