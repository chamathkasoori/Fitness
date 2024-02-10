using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SystemConfig : BaseEntity
{
    public Config Configs { get; set; } = null!;
}
