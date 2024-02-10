namespace Fitness.Dtos;
public class MemberDeviceInformationPostDto
{
    public string DeviceLanguage { get; set; } = null!;
    public string DeviceModel { get; set; } = null!;
    public string DeviceOs { get; set; } = null!;
    public string? DeviceOsVersion { get; set; } 
}