namespace Fitness.Dtos;
public class MemberDeviceInformationDto
{
    public int UserId { get; set; }
    public int MemberId { get; set; }
    public string DeviceLanguage { get; set; } = null!;
    public string DeviceModel { get; set; } = null!;
    public string DeviceOs { get; set; } = null!;
    public string? DeviceOsVersion { get; set; }
}