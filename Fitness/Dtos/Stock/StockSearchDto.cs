namespace Fitness.Dtos;
public class StockSearchDto
{
    public int ClubId { get; set; }
    public int ProductCategoryId { get; set; }
    public bool ShowDeletedProducts { get; set; } = false;
}
