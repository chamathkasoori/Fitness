using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class EmployeeAvailableClub : BaseEntity
{
    public int EmployeeId { get; set; }
    [JsonIgnore]
    public virtual Employee Employee { get; set; } = null!;

    public int ClubId { get; set; }
    [JsonIgnore]
    public virtual Club Club { get; set; } = null!;
}
