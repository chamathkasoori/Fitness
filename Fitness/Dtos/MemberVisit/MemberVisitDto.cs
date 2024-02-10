namespace Fitness.Dtos;
public class MemberVisitDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int ClubId { get; set; }
    public string ClubName { get; set; } = string.Empty;
    public DateTime EnterDate { get; set; }
    public DateTime? LeaveDate { get; set; }
    public AddressDto Address { get; set; } = new AddressDto();
    public bool MemberFeedbackGiven { get; set; }
}
