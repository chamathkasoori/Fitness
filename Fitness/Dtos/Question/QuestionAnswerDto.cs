namespace Fitness.Dtos;
public class QuestionAnswerDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int MemberId { get; set; }
    public bool Answer { get; set; }
}
