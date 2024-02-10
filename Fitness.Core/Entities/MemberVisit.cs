using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberVisit : BaseEntity
{
    public int MemberId { get; set; }

    public int ClubId { get; set; }

    public DateTime EnterDate { get; set; }

    public DateTime? LeaveDate { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Club Club { get; set; } = null!;
}