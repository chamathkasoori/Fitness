namespace Fitness.Dtos;
public class ClubOpeningHourDto
{
    public int DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}
