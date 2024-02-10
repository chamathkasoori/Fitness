namespace Fitness.Dtos;

public class MobileUsageLogPostDto
{
    public int MemberId { get; set; }
    public string Date { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string DeviceOs { get; set; } = null!;
}