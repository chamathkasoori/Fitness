namespace Fitness.Dtos;
public class MemberSessionRatingPostDto
{
    public int MemberId { get; set; }
    public int VisitId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime AddedDate { get; set; }
}