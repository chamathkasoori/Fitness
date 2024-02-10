namespace Fitness.Dtos;
public class AccessRulePostDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IgnoreFingerPrintValidation { get; set; } = false;
}
