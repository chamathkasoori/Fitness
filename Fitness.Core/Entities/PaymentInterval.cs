using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class PaymentInterval : BaseEntity
{
    public string Name { get; set; } = null!;

    public int CompanyId { get; set; }

    public PaymentType Type { get; set; }

    public int Value { get; set; }

    public virtual Company Company { get; set; } = null!;
}
