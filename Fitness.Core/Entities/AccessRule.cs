using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class AccessRule : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public bool IgnoreFingerPrintValidation { get; set; } = false;

    public virtual ICollection<AccessRuleItem> AccessRuleItems { get; set; } = new List<AccessRuleItem>();
}
