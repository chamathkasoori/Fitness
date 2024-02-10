namespace Fitness.Dtos;
public class AuthenticationRoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public string Company { get; set; } = string.Empty;
}
