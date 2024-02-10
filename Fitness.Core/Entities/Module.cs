using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public  class Module : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int? ParentModuleId { get; set; }
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public string Hierarchy { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual Module? ParentModule { get; set; }

    public virtual ICollection<ModuleOperation> ModuleOperations { get; set; } = new List<ModuleOperation>();
    public virtual ICollection<Module>? Modules { get; set; }
    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();
}
