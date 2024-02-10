using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class Inquiry : BaseEntity
{
    public int MemberId { get; set; }
    public int ClubId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string TicketId { get; set; } = string.Empty;
    public InquiryStatus Status { get; set; } = InquiryStatus.Reviewing;
    public string InquiryEntry { get; set; } = string.Empty;
    public string? Attachment { get; set; }
    public bool? IsHappy { get; set; }
    public string? AdminResponse { get; set; }
    public string? MemberComment { get; set; }
    public virtual Member Member { get; set; } = null!;
    public virtual InquiryReply InquiryReply { get; set; } = null!;
    public virtual Club Club { get; set; } = null!;
}
