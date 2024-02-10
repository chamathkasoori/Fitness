using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberQrLog : BaseEntity
{
    public int MemberId { get; set; }

    public int ClubId { get; set; }

    public Guid UniqueIdentifier { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Club Club { get; set; } = null!;
}