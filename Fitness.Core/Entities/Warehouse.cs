using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Warehouse : BaseEntity
{
    public int? ClubId { get; set; }    
    public string Name { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public virtual Club? Club { get; set; }
}