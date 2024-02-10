using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class RoleModule :BaseEntity
{
    public int RoleId { get; set; }
    [JsonIgnore]
    public Role Role { get; set; } = null!;

    public int ModuleId { get; set; }
    [JsonIgnore]
    public Module Module { get; set; } = null!;
    
    public ICollection<RoleModuleOperation> RoleModuleOperations { get; set; } = new List<RoleModuleOperation>();
}
