namespace Fitness.Core.Common.Xero.Contact;
    
public class XeroResponseContactsDto
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateTimeUTC { get; set; }
    public List<XeroResponseContactDto> Contacts { get; set; } = new List<XeroResponseContactDto>();
}

public class XeroResponseContactDto
{
    public Guid ContactID { get; set; }
    public string ContactStatus { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string BankAccountDetails { get; set; } = string.Empty;
    public DateTime UpdatedDateUTC { get; set; }
    public bool IsSupplier { get; set; }
    public bool IsCustomer { get; set; }
    public bool HasValidationErrors { get; set; }
}
