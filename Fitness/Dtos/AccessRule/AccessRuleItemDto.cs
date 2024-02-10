namespace Fitness.Dtos;
public class AccessRuleItemDto
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool AddToAllClubs { get; set; } = false;

    public bool IsActiveForNewClubs { get; set; } = false;

    public List<AccessRuleItemTimingDto> AccessRuleItemTimings { get; set; } = new List<AccessRuleItemTimingDto>();

    public List<AccessRuleItemAssignedClubDto> AccessRuleItemAssignedClubs { get; set; } = new List<AccessRuleItemAssignedClubDto>();
}
