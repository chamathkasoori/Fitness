using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class QuestionComment : BaseEntity
{
    public int MemberId { get; set; } 
    public string Comment { get; set; } = string.Empty;
    public virtual Member Member { get; set; } = null!;
}
