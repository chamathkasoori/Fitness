namespace Fitness.Dtos;
public class SupplierDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TaxIdNumber { get; set; }
    public string? Region { get; set; }
    public string? BankAccount { get; set; }
    public string? Notes { get; set; }
    public bool IsClubRelatedCompany { get; set; }
    public int? AddressId { get; set; }
    public string? ContactPerson { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}