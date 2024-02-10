using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanDiscountApplication : BaseEntity
{
    public int SubscriptionPlanDiscountId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;

    public int ApplicationId { get; set; }
    [JsonIgnore]
    public virtual Application Application { get; set; } = null!;
}
