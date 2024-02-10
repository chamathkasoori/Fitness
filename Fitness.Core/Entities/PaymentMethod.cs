using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<SubscriptionPlanPaymentMethod> SubscriptionPlanPaymentMethods { get; set; } = new List<SubscriptionPlanPaymentMethod>();
}