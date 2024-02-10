using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class QuestionMultiLanguage : BaseEntity
{
    public int QuestionId { get; set; }
    public Languages Language { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public virtual Question Question { get; set; } = null!;
}