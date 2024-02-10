using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Question : BaseEntity
{
    public string DefaultQuestion { get; set; } = string.Empty;
}