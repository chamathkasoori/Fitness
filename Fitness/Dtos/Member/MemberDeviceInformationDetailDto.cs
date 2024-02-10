namespace Fitness.Dtos;
public class MemberDeviceInformationDetailDto: MemberDeviceInformationDto
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}