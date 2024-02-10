using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class ProductAvailableClub : BaseEntity
{
    public int ProductId { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    public int ClubId { get; set; }
    [JsonIgnore]
    public virtual Club Club { get; set; } = null!;
}
