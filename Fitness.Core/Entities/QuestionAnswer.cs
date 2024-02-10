using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class QuestionAnswer : BaseEntity
{
    public int QuestionId { get; set; }
    public int MemberId { get; set; }
    public bool Answer { get; set; }

    public virtual Member Member { get; set; } = null!;
    public virtual Question Question { get; set; } = null!;
}