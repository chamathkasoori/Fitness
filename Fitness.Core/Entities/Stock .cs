using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class Stock : BaseEntity
{
    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int Quantity { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [JsonIgnore]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
