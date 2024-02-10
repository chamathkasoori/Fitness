namespace Fitness.Dtos;
public class MemberValidateDto
{
    public int MemberId { get; set; }
    public int UserId { get; set; }
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PersonalIdentificationNumber { get; set; } = string.Empty;
}