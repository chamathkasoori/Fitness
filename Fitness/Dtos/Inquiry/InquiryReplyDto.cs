namespace Fitness.Dtos;
public class InquiryReplyDto
{
    public int Id { get; set; }
    public int InquiryId { get; set; }
    public string Reply { get; set; } = string.Empty;
    public DateTime RepliedOn { get; set; }
    public string ReplyFrom { get; set; } = string.Empty;
}
