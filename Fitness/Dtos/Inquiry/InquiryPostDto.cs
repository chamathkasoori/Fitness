namespace Fitness.Dtos;
public class InquiryPostDto
{
    public int ClubId { get; set; }
    public string InquiryEntry { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Attachment { get; set; }
}
