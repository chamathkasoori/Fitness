namespace Fitness.Dtos;
public class WarehouseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ClubId { get; set; }
    public bool IsMain { get; set; }
}
