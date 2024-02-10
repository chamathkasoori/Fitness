namespace Fitness.Dtos;
public class CompanyDto
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? AccountNumber { get; set; }
}