using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string Image { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
