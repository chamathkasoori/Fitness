namespace Fitness.Dtos;
public class InquiryDetailsDto : InquiryDto
{
    public string MemberNo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public InquiryReplyDto InquiryReply { get; set; } = null!;
}