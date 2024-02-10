using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberTag : BaseEntity
{
    public int MemberId { get; set; }
    public virtual Member Member { get; set; } = null!;

    public int TagId { get; set; }
    public virtual Tag Tag { get; set; } = null!;
}
