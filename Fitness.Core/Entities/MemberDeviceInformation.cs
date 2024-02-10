using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberDeviceInformation : BaseEntity
{
    public int MemberId { get; set; }
    public string? DeviceModel { get; set; }
    public string? DeviceOs { get; set; }
    public string? DeviceOsVersion { get; set; }
    public string? DeviceLanguage { get; set; }
}