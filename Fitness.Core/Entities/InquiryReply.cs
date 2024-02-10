using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class InquiryReply : BaseEntity
{
    public int InquiryId { get; set; }
    public string Reply { get; set; } = string.Empty;
    public int ReplyFromId { get; set; }
    public Inquiry Inquiry { get; set; } = null!;
    public User ReplyFrom { get; set; } = null!;
}