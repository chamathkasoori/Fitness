using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class ProductPriceAudit : BaseEntity
{
    public int ProductId { get; set; }
    public decimal? GrossPriceOld { get; set; }
    public decimal? GrossPriceNew { get; set; }
    public decimal? NetPriceOld { get; set; }
    public decimal? NetPriceNew { get; set; }
    public virtual Product Product { get; set; } = null!;
}
