namespace Fitness.Dtos;
public class AccessRuleItemPostDto
{
    public string Description { get; set; } = string.Empty;

    public bool IsActiveForNewClubs { get; set; } = false;

    public bool AddToAllClubs { get; set; } = false;

    public List<int> ClubIds { get; set; } = new List<int>();

    public List<AccessRuleItemTimingDto> AccessRuleItemTimings { get; set; } = new List<AccessRuleItemTimingDto>();
}
