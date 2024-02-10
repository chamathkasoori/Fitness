using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class InquiryDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string TicketId { get; set; } = string.Empty;
    public InquiryStatus Status { get; set; } = InquiryStatus.Reviewing;
    public string InquiryEntry { get; set; } = string.Empty;
    public string? Attachment { get; set; }
    public bool? IsHappy { get; set; }
    public string? AdminResponse { get; set; }
    public string? MemberComment { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
}
