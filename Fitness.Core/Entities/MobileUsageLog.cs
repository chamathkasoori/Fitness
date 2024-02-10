using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MobileUsageLog : BaseEntity
{
    public int MemberId { get; set; }
    public string Date { get; set; } = null!;
    public string Time { get; set; } = null!;
    public string DeviceOs { get; set; } = null!;
}