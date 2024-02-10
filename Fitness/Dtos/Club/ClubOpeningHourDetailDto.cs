namespace Fitness.Dtos;
public class ClubOpeningHourDetailDto
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public bool IsClosed { get; set; } = false;
    public string OpenFrom { get; set; } = string.Empty;
    public string OpenUntil { get; set; } = string.Empty;
    public TimeSpan TimeFrom { get; set; }
    public TimeSpan TimeUntil { get; set; }
}
