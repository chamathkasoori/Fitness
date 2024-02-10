using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class ProductAvailableApplication : BaseEntity
{
    public int ProductId { get; set; }

    public int ApplicationId { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [JsonIgnore]
    public virtual Application Application { get; set; } = null!;
}
