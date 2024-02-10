namespace Fitness.Dtos;
public class AccessRuleItemTimingDto
{
    public int DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}
