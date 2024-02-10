namespace Fitness.Dtos;
public class ProductSearchDto
{
    public string Name { get; set; } = string.Empty;
    public int ClubId { get; set; }
    public int ApplicationId { get; set; }
    public int ProductCategoryId { get; set; }
    public bool ShowDeletedProducts { get; set; } = false;
}
