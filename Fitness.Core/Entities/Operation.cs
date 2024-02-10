using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;

public class Operation : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<ModuleOperation> Modules { get; set; } = new List<ModuleOperation>();
}