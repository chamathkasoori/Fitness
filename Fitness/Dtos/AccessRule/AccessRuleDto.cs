namespace Fitness.Dtos;
public class AccessRuleDto
{
    public int Id { get; set; }

    public bool IsActive { get; set; } = true;

    public string Name { get; set; } = string.Empty;

    public bool IgnoreFingerPrintValidation { get; set; } = false;

    public List<AccessRuleItemDto> AccessRuleItems { get; set; } = new List<AccessRuleItemDto>();
}
