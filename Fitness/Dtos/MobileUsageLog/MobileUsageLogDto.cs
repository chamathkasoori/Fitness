namespace Fitness.Dtos;
public class MobileUsageLogDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string Date { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string DeviceOs { get; set; } = null!;
}