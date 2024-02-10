using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class ModuleOperation : BaseEntity
{
    public int ModuleId { get; set; }

    public int OperationId { get; set; }

    [JsonIgnore]
    public Module Module { get; set; } = null!;
    
    [JsonIgnore]
    public Operation Operation { get; set; } = null!;
}
