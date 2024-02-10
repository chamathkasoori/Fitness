using Fitness.Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Fitness.Core.Entities;
public class StockMovement : BaseEntity
{
    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public int Qty { get; set; }

    public string Adjustment { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [JsonIgnore]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
