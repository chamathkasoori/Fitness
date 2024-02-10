using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Application : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    /*
    public string Platform { get; set; } = null!;

    public virtual ICollection<SubscriptionPlanApplication> SubscriptionPlanApplications { get; set; } =
        new List<SubscriptionPlanApplication>();
    */
}