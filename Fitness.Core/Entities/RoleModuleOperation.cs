using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class RoleModuleOperation : BaseEntity
{
    public int RoleModuleId { get; set; }
    public int OperationId { get; set; }

    [JsonIgnore]
    public virtual RoleModule RoleModule { get; set; } = null!;
    public virtual Operation Operation { get; set; } = null!;
}