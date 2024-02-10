namespace Fitness.Dtos;
public class MemberOtpDto
{
    public bool Status { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public int MemberId { get; set; }
    public int ClubId { get; set; }
}