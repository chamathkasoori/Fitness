namespace Fitness.Dtos;
public class QuestionAnswerPostDto
{
    public List<AnswerPost> Answers { get; set; } = new List<AnswerPost>();
    public string Comment { get; set; } = string.Empty;
}

public class AnswerPost
{
    public int QuestionId { get; set; }
    public bool Answer { get; set; }
}
