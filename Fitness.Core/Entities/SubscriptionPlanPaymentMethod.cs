using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanPaymentMethod : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    [JsonIgnore]
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int PaymentMethodId { get; set; }
    [JsonIgnore]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
