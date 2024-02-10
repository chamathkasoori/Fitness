using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSessionRating : BaseEntity
{
    public int MemberId { get; set; }
    public int VisitId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
